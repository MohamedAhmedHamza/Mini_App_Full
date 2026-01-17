document.addEventListener("DOMContentLoaded", () => {
    // لو مسجل بالفعل → حوله
    const token = sessionStorage.getItem("token");
    if (token) {
        const user = Auth.parseJwt(token);
        if (user) {
            Auth.redirectByRole(user);
            return;
        }
    }

    const form = document.getElementById("login-form");
    const error = document.getElementById("error");

    form.addEventListener("submit", async (e) => {
        e.preventDefault();
        try {
            const user = await Auth.login(
                username.value.trim(),
                password.value.trim()
            );
            Auth.redirectByRole(user);
        } catch (err) {
            error.innerText = err.message;
        }
    });
});
