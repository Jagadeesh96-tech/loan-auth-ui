
import axios from "axios";

const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

export const registerUser = async (userData) => {
  const response = await api.post("/api/Auth/register", userData);
  return response.data;
};

export const loginUser = async (loginData) => {
  const response = await api.post("/api/Auth/login", loginData);
  return response.data;
};
