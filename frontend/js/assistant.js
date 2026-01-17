document.addEventListener("DOMContentLoaded", async () => {
    const user = window.Auth.requireAuth("Assistant");
    if (!user) return;

    // Navbar
    document.getElementById("nav-username").innerText =
        user.unique_name || user.username;

    document.getElementById("nav-role").innerText = user.role;

    const ticketsList = document.getElementById("tickets");

    async function loadTickets() {
        try {
            const res = await window.Auth.authorizedFetch(
                `${window.Auth.API_BASE_URL}/tickets`
            );

            if (!res) return;

            const tickets = await res.json();
            ticketsList.innerHTML = "";

            tickets.forEach(t => {
                const li = document.createElement("li");
                li.innerHTML = `
                    <strong>${t.userName}</strong> |
                    ${t.title} |
                    <span class="status ${t.status.toLowerCase()}">
                        ${t.status}
                    </span>
                `;
                ticketsList.appendChild(li);
            });
        } catch (err) {
            console.error(err);
        }
    }

    loadTickets();
    setInterval(loadTickets, 5000);
});
