document.addEventListener("DOMContentLoaded", async () => {
    const user = window.Auth.requireAuth("User");
    if (!user) return;

    // Navbar
    const navUsername = document.getElementById("nav-username");
    const navRole = document.getElementById("nav-role");
    if (navUsername) navUsername.innerText = user.unique_name || user.username;
    if (navRole) navRole.innerText = user.role;

    const ticketsList = document.getElementById("tickets");
    const ticketForm = document.getElementById("ticketForm");

    // إرسال تذكرة جديدة
    ticketForm.addEventListener("submit", async (e) => {
        e.preventDefault();
        try {
            await window.Auth.authorizedFetch(`${window.Auth.API_BASE_URL}/tickets`, {
                method: "POST",
                body: JSON.stringify({
                    title: document.getElementById("title").value,
                    description: document.getElementById("description").value,
                    status: "Pending"
                })
            });

            ticketForm.reset();
            loadTickets();
        } catch (err) {
            alert(err.message);
        }
    });

    // تحميل تذاكر المستخدم
    async function loadTickets() {
        try {
            const res = await window.Auth.authorizedFetch(`${window.Auth.API_BASE_URL}/tickets`);
            if (!res) return;

            const tickets = await res.json();
            ticketsList.innerHTML = "";

            tickets.forEach(t => {
                const li = document.createElement("li");
                li.innerHTML = `${t.title}  <span class="status ${t.status.toLowerCase()}">${t.status}</span>`;
                ticketsList.appendChild(li);
            });
        } catch (err) {
            console.error(err);
        }
    }

    loadTickets();
    setInterval(loadTickets, 5000);
});
