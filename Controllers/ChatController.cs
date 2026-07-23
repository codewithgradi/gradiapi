using GradiApi.MCP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Protocol;
[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
  private readonly IChatClient _chatClient;
  private readonly McpPersonalTool _mcpPersonalTool;

  public ChatController(IChatClient chatClient, McpPersonalTool mcpPersonalTool)
  {
    _chatClient = chatClient;
    _mcpPersonalTool = mcpPersonalTool;
  }

  public record ChatRequest(string message);
  [HttpPost]
  public async Task<IActionResult> Chat([FromBody] ChatRequest request)
  {
    if (string.IsNullOrWhiteSpace(request.message))
    {
      return BadRequest("Message can not be empty");
    }
    var tools = new List<AITool>
    {
      AIFunctionFactory.Create(_mcpPersonalTool.GetBasicInfo),
      AIFunctionFactory.Create(_mcpPersonalTool.GetInTouch)
    };

    var history = new List<ChatMessage>
    {
      new(ChatRole.System, ""),
      new(ChatRole.User,request.message)
    };

    var options = new ChatOptions { Tools = tools };
    var response = await _chatClient.GetResponseAsync(history, options);

    return Ok(new { reply = response.Text });
  }

}