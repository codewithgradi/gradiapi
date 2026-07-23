using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;
using GradiApi.MCP;

namespace GradiApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatClient _chatClient;
    private readonly McpPersonalTool _mcpTool;
    private readonly ILogger<ChatController> _logger;

    public ChatController(
        IChatClient chatClient,
        McpPersonalTool mcpTool,
        ILogger<ChatController> logger)
    {
        _chatClient = chatClient;
        _mcpTool = mcpTool;
        _logger = logger;
    }

    /// <summary>
    /// Sends a prompt to the AI model, allowing execution of configured MCP tools.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Chat([FromBody] ChatRequest request, CancellationToken cancellationToken)
    {
        // 1. Basic Request Validation
        if (string.IsNullOrWhiteSpace(request.Message))
        {
            return BadRequest(new ChatResponseDto(
                Success: false,
                Reply: null,
                Error: "Message prompt cannot be empty."
            ));
        }

        _logger.LogInformation("Processing chat request with prompt: {Prompt}", request.Message);

        // 2. Define System Role & Conversation Context
        List<ChatMessage> conversation = new()
        {
            new ChatMessage(ChatRole.System, """
                You are Gradi's AI assistant integrated with personal MCP tools.
                ALWAYS check and call your available tools (like GetBasicInfo or GetInTouch) to find information about Gradi before answering questions about him.
                Never state that you lack information without invoking your tools first.
                """),
            new ChatMessage(ChatRole.User, request.Message)
        };

        // 3. Register Available Tools / AI Functions
        var chatOptions = new ChatOptions
        {
            Tools = new List<AITool>
            {
                AIFunctionFactory.Create(
                    async ([Description("Search query or personal details filter")] string query) =>
                    {
                        return await _mcpTool.GetBasicInfo();
                    },
                    name: "GetBasicInfo",
                    description: "Fetches basic profile information, background, and personal context about Gradi."
                ),
                AIFunctionFactory.Create(
                    async ([Description("Search query for contact methods")] string query) =>
                    {
                        return await _mcpTool.GetInTouch();
                    },
                    name: "GetInTouch",
                    description: "Fetches contact details, social links, or communication preferences for Gradi."
                )
            }
        };

        // 4. Send Request to Model
        ChatResponse response = await _chatClient.GetResponseAsync(
            conversation,
            chatOptions,
            cancellationToken
        );

        _logger.LogInformation("Chat completion successful.");

        return Ok(new ChatResponseDto(
            Success: true,
            Reply: response.Text,
            Error: null
        ));
    }
}

public record ChatRequest(string Message);

public record ChatResponseDto(
    bool Success,
    string? Reply,
    string? Error
);