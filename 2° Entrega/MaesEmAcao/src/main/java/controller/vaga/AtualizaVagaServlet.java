package controller.vaga;

import java.io.IOException;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import dao.VagaDAO;
import model.Empresa;
import model.Vaga;

@WebServlet(urlPatterns = { "/vaga-editar", "/vaga-atualizar" })
public class AtualizaVagaServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;
	VagaDAO vagaDao = new VagaDAO();
	Vaga vaga = new Vaga();
	Empresa empresa = new Empresa();

	public AtualizaVagaServlet() {
		super();

	}

	protected void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		String action = request.getServletPath();

		switch (action) {
		case "/vaga-editar":
			editar(request, response);
			break;
		case "/vaga-atualizar":
			atualizar(request, response);
			break;
		default:
			response.sendRedirect("index.jsp");
			break;
		}
	}

	private void editar(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		int id = Integer.parseInt(request.getParameter("id"));
		vaga = vagaDao.consultarById(id);

		request.setAttribute("vaga", vaga);
		RequestDispatcher rd = request.getRequestDispatcher("./views/vaga/atualizacaoVaga.jsp");
		rd.forward(request, response);

	}

	private void atualizar(HttpServletRequest request, HttpServletResponse response) throws IOException {
		int id = Integer.parseInt(request.getParameter("idEmpresa"));
		empresa.setIdEmpresa(id);
		vaga.setTitulo(request.getParameter("titulo"));
		vaga.setDescricao(request.getParameter("descricao"));
		vaga.setRequisito(request.getParameter("requisito"));
		String salarioParam = request.getParameter("salario");
		System.out.println("Valor do salário recebido: " + salarioParam);

		String str = salarioParam.replace(",", ".");
		System.out.println("Valor após substituir vírgulas por pontos: " + str);

		try {
			Double sal = Double.parseDouble(str);
			vaga.setSalario(sal);
		} catch (NumberFormatException e) {
			System.out.println("Erro ao converter para Double: " + e.getMessage());
			// Trate o erro conforme necessário
		}
		int idVaga = Integer.parseInt(request.getParameter("idVaga"));
		vaga.setIdVaga(idVaga);
		vagaDao.atualizar(vaga);

		response.sendRedirect("vaga?id=" + id);

	}

}
