import { useEffect, useState } from "react";
import {
  listarPessoas,
  criarPessoa,
  removerPessoa,
  listarTelefonesPorPessoa,
  criarTelefone,
} from "./api";
import "./index.css";

function App() {
  const [pessoas, setPessoas] = useState([]);
  const [nome, setNome] = useState("");
  const [email, setEmail] = useState("");
  const [dataNascimento, setDataNascimento] = useState("");
  const [telefonesSelecionados, setTelefonesSelecionados] = useState({});
  const [novoTelefoneNumero, setNovoTelefoneNumero] = useState("");
  const [novoTelefoneTipo, setNovoTelefoneTipo] = useState("");
  const [pessoaParaTelefone, setPessoaParaTelefone] = useState(null);
  const [carregando, setCarregando] = useState(false);

  const carregarPessoas = async () => {
    setCarregando(true);
    try {
      const resposta = await listarPessoas();
      setPessoas(resposta.data);
    } catch (erro) {
      console.error("Erro ao carregar pessoas:", erro);
      alert("Erro ao carregar pessoas");
    } finally {
      setCarregando(false);
    }
  };

  useEffect(() => {
    carregarPessoas();
  }, []);

  const handleCriarPessoa = async (e) => {
    e.preventDefault();
    if (!nome) {
      alert("Nome é obrigatório");
      return;
    }

    const payload = {
      nome,
      email: email || null,
      dataNascimento: dataNascimento || null,
    };

    try {
      await criarPessoa(payload);
      setNome("");
      setEmail("");
      setDataNascimento("");
      await carregarPessoas();
    } catch (erro) {
      console.error("Erro ao criar pessoa:", erro);
      alert("Erro ao criar pessoa");
    }
  };

  const handleRemover = async (id) => {
    if (!window.confirm("Deseja realmente excluir esta pessoa?")) return;

    try {
      await removerPessoa(id);
      await carregarPessoas();
    } catch (erro) {
      console.error("Erro ao remover pessoa:", erro);
      alert("Erro ao remover pessoa");
    }
  };

  const handleVerTelefones = async (pessoaId) => {
    try {
      const resposta = await listarTelefonesPorPessoa(pessoaId);
      setTelefonesSelecionados((anterior) => ({
        ...anterior,
        [pessoaId]: resposta.data,
      }));
    } catch (erro) {
      console.error("Erro ao carregar telefones:", erro);
      alert("Erro ao carregar telefones");
    }
  };

  const abrirFormularioTelefone = (pessoaId) => {
    setPessoaParaTelefone(pessoaId);
    setNovoTelefoneNumero("");
    setNovoTelefoneTipo("");
  };

  const handleCriarTelefone = async (e) => {
    e.preventDefault();
    if (!pessoaParaTelefone || !novoTelefoneNumero) {
      alert("Número do telefone é obrigatório.");
      return;
    }

    const payload = {
      numero: novoTelefoneNumero,
      tipo: novoTelefoneTipo || null,
      pessoaId: pessoaParaTelefone,
    };

    try {
      await criarTelefone(payload);
      setNovoTelefoneNumero("");
      setNovoTelefoneTipo("");
      setPessoaParaTelefone(null);
      await handleVerTelefones(payload.pessoaId);
    } catch (erro) {
      console.error("Erro ao criar telefone:", erro);
      alert("Erro ao criar telefone");
    }
  };

  return (
    <div
      style={{
        maxWidth: 900,
        margin: "0 auto",
        padding: 16,
        backgroundColor: "white",
        minHeight: "100vh",
      }}
    >
      <h1>Desafio LAR CRUD</h1>

      <section style={{ marginBottom: 24 }}>
        <h2>Cadastrar Pessoa</h2>
        <form onSubmit={handleCriarPessoa}>
          <div style={{ marginBottom: 8 }}>
            <label>
              Nome:{" "}
              <input
                value={nome}
                onChange={(e) => setNome(e.target.value)}
                placeholder="Nome completo"
              />
            </label>
          </div>
          <div style={{ marginBottom: 8 }}>
            <label>
              E-mail:{" "}
              <input
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                placeholder="email@exemplo.com"
              />
            </label>
          </div>
          <div style={{ marginBottom: 8 }}>
            <label>
              Data de nascimento:{" "}
              <input
                type="date"
                value={dataNascimento}
                onChange={(e) => setDataNascimento(e.target.value)}
              />
            </label>
          </div>
          <button type="submit">Salvar pessoa</button>
        </form>
      </section>

      <section>
        <h2>Lista de pessoas</h2>
        {carregando && <p>Carregando...</p>}
        {!carregando && pessoas.length === 0 && (
          <p>Nenhuma pessoa cadastrada.</p>
        )}

        <ul style={{ listStyle: "none", padding: 0 }}>
          {pessoas.map((pessoa) => (
            <li
              key={pessoa.id}
              style={{
                marginBottom: 12,
                padding: 12,
                border: "1px solid #ddd",
                borderRadius: 4,
              }}
            >
              <div style={{ display: "flex", justifyContent: "space-between" }}>
                <div>
                  <strong>{pessoa.nome}</strong>
                  <div>
                    {pessoa.email || "sem e-mail"}{" "}
                    {pessoa.dataNascimento &&
                      `- nasc.: ${new Date(
                        pessoa.dataNascimento
                      ).toLocaleDateString("pt-BR")}`}
                  </div>
                </div>
                <div>
                  <button onClick={() => handleVerTelefones(pessoa.id)}>
                    Ver telefones
                  </button>{" "}
                  <button onClick={() => abrirFormularioTelefone(pessoa.id)}>
                    Adicionar telefone
                  </button>{" "}
                  <button onClick={() => handleRemover(pessoa.id)}>
                    Excluir
                  </button>
                </div>
              </div>

              {telefonesSelecionados[pessoa.id] && (
                <ul style={{ marginTop: 8 }}>
                  {telefonesSelecionados[pessoa.id].length === 0 && (
                    <li>Nenhum telefone cadastrado.</li>
                  )}
                  {telefonesSelecionados[pessoa.id].map((tel) => (
                    <li key={tel.id}>
                      {tel.numero} ({tel.tipo || "tipo não informado"})
                    </li>
                  ))}
                </ul>
              )}

              {pessoaParaTelefone === pessoa.id && (
                <form onSubmit={handleCriarTelefone} style={{ marginTop: 8 }}>
                  <div style={{ marginBottom: 4 }}>
                    <label>
                      Número:{" "}
                      <input
                        value={novoTelefoneNumero}
                        onChange={(e) =>
                          setNovoTelefoneNumero(e.target.value)
                        }
                        placeholder="(99) 99999-9999"
                      />
                    </label>
                  </div>
                  <div style={{ marginBottom: 4 }}>
                    <label>
                      Tipo:{" "}
                      <input
                        value={novoTelefoneTipo}
                        onChange={(e) => setNovoTelefoneTipo(e.target.value)}
                        placeholder="Celular, Fixo, WhatsApp..."
                      />
                    </label>
                  </div>
                  <button type="submit">Salvar telefone</button>{" "}
                  <button
                    type="button"
                    onClick={() => setPessoaParaTelefone(null)}
                  >
                    Cancelar
                  </button>
                </form>
              )}
            </li>
          ))}
        </ul>
      </section>
    </div>
  );
}

export default App;
