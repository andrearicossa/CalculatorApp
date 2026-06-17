# Proposal: Calculator History (Session)

Implement a session-based operation history cleared by Clear, shown in a scrollable Bootstrap modal.

Key points:
- Store history server-side in HttpContext.Session (JSON list)
- Append entry after successful calculation
- Add History button + Bootstrap modal with scrollbar
- Clear removes session history
