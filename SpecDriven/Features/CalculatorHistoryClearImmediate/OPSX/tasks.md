# Tasks: Immediate History Clear

1. Ensure server-side clear is scoped to history only.
2. Update Clear client-side function to:
   - clear calculator state
   - call clear-history handler
   - update modal content to show "No history" immediately
3. Manual test:
   - Do calculations
   - Open History modal
   - Press Clear and confirm modal shows empty immediately
   - Close/open modal and confirm empty
