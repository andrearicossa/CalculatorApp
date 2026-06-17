# Design: Immediate History Clear

Approach:
- Keep clearing history on the server as today.
- Update the History modal DOM immediately on Clear (client-side) so it shows "No history" right away.

UX decision:
- If modal is open, keep it open and update its content.

Notes:
- Clear should remove only History data (avoid clearing unrelated session data).
