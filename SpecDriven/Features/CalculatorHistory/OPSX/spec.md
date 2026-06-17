# Spec: Calculator History

Acceptance:
- Successful `=` adds an entry `A op B = Result`.
- History is kept for the session across POST reloads.
- History is shown via a scrollable Bootstrap modal opened by a History button.
- Closing modal returns to calculator unchanged.
- Clear also clears history.

Implementation:
- Use HttpContext.Session with JSON list.
- Append after successful calculation in OnPost.
- Add handler to clear session history.
