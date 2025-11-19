import axios from "axios";

// A API está em http://localhost:5000 (sem HTTPS)
const api = axios.create({
  baseURL: "http://localhost:5000/api",
});

// Pessoas
export const listarPessoas = () => api.get("/pessoas");
export const criarPessoa = (pessoa) => api.post("/pessoas", pessoa);
export const removerPessoa = (id) => api.delete(`/pessoas/${id}`);

// Telefones
export const listarTelefonesPorPessoa = (pessoaId) =>
  api.get(`/telefones/pessoa/${pessoaId}`);

export const criarTelefone = (telefone) => api.post("/telefones", telefone);

export default api;
