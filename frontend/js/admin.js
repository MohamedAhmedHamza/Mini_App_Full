document.addEventListener("DOMContentLoaded", async () => {
    const user = window.Auth.requireAuth("Admin");
    if (!user) return;

    // Navbar
    const navUsername = document.getElementById("nav-username");
    const navRole = document.getElementById("nav-role");

    if (navUsername)
        navUsername.innerText = user.username || user.unique_name || "Unknown User";

    if (navRole)
        navRole.innerText = user.role || "";

    const ticketsList = document.getElementById("tickets");

    async function loadTickets() {
        try {
            const res = await window.Auth.authorizedFetch(
                `${window.Auth.API_BASE_URL}/tickets`
            );

            const tickets = await res.json();
            ticketsList.innerHTML = "";

            tickets.forEach(t => {
                const li = document.createElement("li");

                li.innerHTML = `
                    <strong>${t.userName ?? "Unknown User"}</strong>
                    | ${t.title}
                    | <span class="status ${t.status.toLowerCase()}">${t.status}</span>

                    ${
                        t.status === "Pending" && user.role === "Admin"
                            ? `
                        <button onclick="handleTicket(${t.id}, 'Approved')">Approve</button>
                        <button onclick="handleTicket(${t.id}, 'Rejected')">Reject</button>
                        `
                            : ""
                    }
                `;

                ticketsList.appendChild(li);
            });
        } catch (err) {
            console.error(err);
        }
    }

    async function handleTicket(id, status) {
        try {
            await window.Auth.authorizedFetch(
                `${window.Auth.API_BASE_URL}/admin/tickets/${id}/${status.toLowerCase()}`,
                { method: "POST" }
            );
            loadTickets();
        } catch (err) {
            alert(err.message);
        }
    }

    // تحميل أول مرة
    loadTickets();

    // تحديث كل 5 ثواني (مرة واحدة بس)
    setInterval(loadTickets, 5000);
});
