window.Auth = {
    API_BASE_URL: "https://localhost:7179/api",
    TOKEN_KEY: "token",

    /* ===== TOKEN ===== */
    saveToken(token) {
        sessionStorage.setItem(this.TOKEN_KEY, token);
    },
    getToken() {
        return sessionStorage.getItem(this.TOKEN_KEY);
    },
    logout() {
        sessionStorage.removeItem(this.TOKEN_KEY);
        window.location.href = "/html/login.html";
    },

    /* ===== JWT ===== */
    parseJwt(token) {
        if (!token) return null;
        try {
            return JSON.parse(atob(token.split(".")[1]));
        } catch {
            return null;
        }
    },

    getCurrentUser() {
        return this.parseJwt(this.getToken());
    },

    /* ===== GUARD ===== */
    requireAuth(role = null) {
        const user = this.getCurrentUser();
        if (!user) {
            this.logout();
            return null;
        }
        if (role && user.role !== role) {
            alert("Access Denied");
            this.logout();
            return null;
        }
        return user;
    },

    /* ===== FETCH ===== */
    async authorizedFetch(url, options = {}) {
        const token = this.getToken();
        if (!token) this.logout();

        const res = await fetch(url, {
            ...options,
            headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${token}`,
                ...(options.headers || {})
            }
        });

        if (res.status === 401) this.logout();
        return res;
    },

    /* ===== LOGIN ===== */
    async login(username, password) {
        const res = await fetch(`${this.API_BASE_URL}/auth/login`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ username, password })
        });

        if (!res.ok) throw new Error("Invalid username or password");

        const { token } = await res.json();
        this.saveToken(token);
        return this.parseJwt(token);
    },

    redirectByRole(user) {
        if (user.role === "Admin")
            location.href = "/html/admin/dashboard.html";
        else if (user.role === "Assistant")
            location.href = "/html/assistant/dashboard.html";
        else
            location.href = "/html/user/dashboard.html";
    }
};
