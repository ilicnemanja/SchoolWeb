function deleteConfirmation(event) {
    const choice = window.confirm("Are you sure you want to delete?")
    if (choice == false) {
        event.preventDefault();
        return false
    }
}