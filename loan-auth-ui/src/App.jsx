
import { useState } from "react";
import {
  ShieldCheck,
  User,
  Mail,
  Lock,
  Eye,
  EyeOff,
  ArrowRight,
  CheckCircle,
  AlertCircle,
  Landmark,
  LoaderCircle,
} from "lucide-react";

import { registerUser, loginUser } from "./services/authService";
import "./App.css";

function App() {
  const [mode, setMode] = useState("login");
  const [showPassword, setShowPassword] = useState(false);
  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState("");
  const [error, setError] = useState("");
  const [token, setToken] = useState(
    localStorage.getItem("accessToken")
  );

  const [form, setForm] = useState({
    userName: "",
    fullName: "",
    email: "",
    password: "",
    confirmPassword: "",
  });

  const handleChange = (e) => {
    setForm({
      ...form,
      [e.target.name]: e.target.value,
    });
  };

  const switchMode = (newMode) => {
    setMode(newMode);
    setError("");
    setMessage("");
    setShowPassword(false);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    setError("");
    setMessage("");

    if (mode === "register") {
      if (form.password !== form.confirmPassword) {
        setError("Passwords do not match.");
        return;
      }

      if (form.password.length < 8) {
        setError("Password must contain at least 8 characters.");
        return;
      }
    }

    setLoading(true);

    try {
      if (mode === "register") {
        const payload = {
          userName: form.userName,
          email: form.email,
          password: form.password,
          confirmPassword: form.confirmPassword,
          fullName: form.fullName,
        };

        const result = await registerUser(payload);

        setMessage(
          result?.message || "Registration successful! Please login."
        );

        setForm((previous) => ({
          ...previous,
          password: "",
          confirmPassword: "",
        }));

        setMode("login");
      } else {
        const payload = {
          email: form.email,
          password: form.password,
        };

        const result = await loginUser(payload);

        const accessToken = result?.accessToken;

        if (!accessToken) {
          throw new Error(
            "Login succeeded but accessToken was not found in the response."
          );
        }

        localStorage.setItem("accessToken", accessToken);

        setToken(accessToken);

        setMessage("Login successful!");
      }
    } catch (err) {
      const data = err.response?.data;

      let errorMessage =
        data?.message ||
        data?.title ||
        err.message ||
        "Something went wrong. Please try again.";

      if (data?.errors) {
        errorMessage = Object.values(data.errors)
          .flat()
          .join(" ");
      }

      setError(errorMessage);
    } finally {
      setLoading(false);
    }
  };

  const handleLogout = () => {
    localStorage.removeItem("accessToken");
    setToken(null);
    setForm({
      userName: "",
      fullName: "",
      email: "",
      password: "",
      confirmPassword: "",
    });
    setMessage("You have been logged out.");
    setError("");
  };

  if (token) {
    return (
      <div className="auth-page">
        <div className="success-card">
          <div className="success-icon">
            <CheckCircle size={42} />
          </div>

          <h1>Welcome back!</h1>
          <p>You are successfully authenticated.</p>

          <div className="token-info">
            <ShieldCheck size={20} />
            JWT access token stored successfully.
          </div>

          <button className="primary-btn" onClick={handleLogout}>
            Logout
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="auth-page">

      <div className="auth-container">

        {/* LEFT SIDE */}

        <section className="brand-section">

          <div className="brand-logo">
            <Landmark size={30} />
            <span>LoanSphere</span>
          </div>

          <div className="brand-content">
            <span className="brand-tag">
              SMART FINANCIAL MANAGEMENT
            </span>

            <h1>
              Your financial journey,
              <span> made simpler.</span>
            </h1>

            <p>
              Securely manage your loan account and take
              control of your financial future.
            </p>

            <div className="feature-list">
              <div>
                <ShieldCheck />
                <span>Secure authentication</span>
              </div>

              <div>
                <CheckCircle />
                <span>Simple account management</span>
              </div>

              <div>
                <Lock />
                <span>JWT-based security</span>
              </div>
            </div>
          </div>

          <div className="brand-footer">
            © 2026 LoanSphere. All rights reserved.
          </div>

        </section>

        {/* RIGHT SIDE */}

        <section className="form-section">

          <div className="form-wrapper">

            <div className="mobile-logo">
              <Landmark size={25} />
              LoanSphere
            </div>

            <div className="form-heading">
              <span className="welcome-text">
                {mode === "login"
                  ? "WELCOME BACK"
                  : "GET STARTED"}
              </span>

              <h2>
                {mode === "login"
                  ? "Sign in to your account"
                  : "Create your account"}
              </h2>

              <p>
                {mode === "login"
                  ? "Enter your details to access your account."
                  : "Fill in your details to create a new account."}
              </p>
            </div>

            <div className="tab-switch">
              <button
                className={mode === "login" ? "active" : ""}
                onClick={() => switchMode("login")}
                type="button"
              >
                Sign In
              </button>

              <button
                className={mode === "register" ? "active" : ""}
                onClick={() => switchMode("register")}
                type="button"
              >
                Register
              </button>
            </div>

            {message && (
              <div className="alert success-alert">
                <CheckCircle size={18} />
                {message}
              </div>
            )}

            {error && (
              <div className="alert error-alert">
                <AlertCircle size={18} />
                {error}
              </div>
            )}

            <form onSubmit={handleSubmit}>

              {mode === "register" && (
                <>
                  <div className="input-group">
                    <label>Full Name</label>
                    <div className="input-wrapper">
                      <User size={19} />
                      <input
                        type="text"
                        name="fullName"
                        placeholder="Enter your full name"
                        value={form.fullName}
                        onChange={handleChange}
                        required
                      />
                    </div>
                  </div>

                  <div className="input-group">
                    <label>Username</label>
                    <div className="input-wrapper">
                      <User size={19} />
                      <input
                        type="text"
                        name="userName"
                        placeholder="Choose a username"
                        value={form.userName}
                        onChange={handleChange}
                        required
                      />
                    </div>
                  </div>
                </>
              )}

              <div className="input-group">
                <label>Email Address</label>
                <div className="input-wrapper">
                  <Mail size={19} />
                  <input
                    type="email"
                    name="email"
                    placeholder="name@example.com"
                    value={form.email}
                    onChange={handleChange}
                    required
                  />
                </div>
              </div>

              <div className="input-group">
                <label>Password</label>
                <div className="input-wrapper">
                  <Lock size={19} />
                  <input
                    type={showPassword ? "text" : "password"}
                    name="password"
                    placeholder="Enter your password"
                    value={form.password}
                    onChange={handleChange}
                    required
                  />

                  <button
                    type="button"
                    className="eye-btn"
                    onClick={() => setShowPassword(!showPassword)}
                  >
                    {showPassword
                      ? <EyeOff size={19} />
                      : <Eye size={19} />}
                  </button>
                </div>
              </div>

              {mode === "register" && (
                <div className="input-group">
                  <label>Confirm Password</label>
                  <div className="input-wrapper">
                    <Lock size={19} />
                    <input
                      type="password"
                      name="confirmPassword"
                      placeholder="Confirm your password"
                      value={form.confirmPassword}
                      onChange={handleChange}
                      required
                    />
                  </div>
                </div>
              )}

              {mode === "login" && (
                <div className="form-options">
                  <span>Secure Login</span>
                  <ShieldCheck size={17} />
                </div>
              )}

              <button
                type="submit"
                className="primary-btn"
                disabled={loading}
              >
                {loading ? (
                  <>
                    <LoaderCircle className="spinner" size={20} />
                    Please wait...
                  </>
                ) : (
                  <>
                    {mode === "login"
                      ? "Sign In"
                      : "Create Account"}
                    <ArrowRight size={19} />
                  </>
                )}
              </button>

            </form>

            <div className="bottom-text">
              {mode === "login"
                ? "Don't have an account?"
                : "Already have an account?"}

              <button
                type="button"
                onClick={() =>
                  switchMode(
                    mode === "login" ? "register" : "login"
                  )
                }
              >
                {mode === "login" ? "Create account" : "Sign in"}
              </button>
            </div>

          </div>
        </section>

      </div>
    </div>
  );
}

export default App;