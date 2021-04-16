using System;

namespace Series
{
    class Program
    {

        static public SerieRepositorio repositorio= new SerieRepositorio();
        static void Main(string[] args)
        {
            
            Series.LeArqSeries.MontaRepositorio();
            
            string opcaoUsuario= ObterOpcaoUsuario();



            while (opcaoUsuario.ToUpper() != "X")
            {
                switch(opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;

                    default:
                        Console.Write("Programa Encerrado.\nObrigado por usar nosso serviço");
                        throw new ArgumentOutOfRangeException();
                }
                opcaoUsuario= ObterOpcaoUsuario();
            }
        }

        private static void ListarSeries()
        {
            Console.WriteLine("Listar Séries");

            var lista= repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("\tNenhuma série cadastrada.");
                return;
            }

            Console.WriteLine("\tId\t-\tTítulo");
            foreach (var serie in lista)
            {
                var excluido= serie.retornaExcluido();
                Console.WriteLine("\t{0}:\t-\t{1}\t\t{2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Este título foi removido do acervo*" : ""));
            }
        }

        private static void InserirSerie()
        {
            Console.WriteLine("\tInserir nova série");

            foreach(int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.Write("\tDigite o gênero entre as opções acima: ");
            int entradaGenero= int.Parse(Console.ReadLine());

            Console.Write("\tDigite o título da série: ");
            string entradaTitulo= Console.ReadLine();

            Console.Write("\tDigite o ano de início da série: ");
            int entradaAno= int.Parse(Console.ReadLine());

            Console.Write("\tDigite a descrição da série: ");
            string entradaDescricao= Console.ReadLine();

            Serie novaSerie= new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao,
                                        excluido: false);

            repositorio.Insere(novaSerie);
        }

        private static void AtualizarSerie()
        {
            Console.Write("\tDigite o Id da série: ");
            int indiceSerie= int.Parse(Console.ReadLine());

            foreach(int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero),i));
            }
            Console.Write("\tDigite o gênero entre as opções acima: ");
            int entradaGenero= int.Parse(Console.ReadLine());

            Console.Write("\tDigite o título da série: ");
            string entradaTitulo= Console.ReadLine();

            Console.Write("\tDigite o ano de início da série: ");
            int entradaAno= int.Parse(Console.ReadLine());

            Console.Write("\tDigite a descrição da série: ");
            string entradaDescricao= Console.ReadLine();

            Serie atualizaSerie= new Serie(id: indiceSerie,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao,
                                        excluido: false);

            repositorio.Atualiza(indiceSerie, atualizaSerie);
        }

        private static void ExcluirSerie()
        {
            Console.Write("\tDigite o id da série: ");
            int indiceSerie= int.Parse(Console.ReadLine());
            
            repositorio.Exclui(indiceSerie);
        }

        private static void VisualizarSerie()
        {
            Console.Write("\tDigite o id da série: ");
            int indiceSerie= int.Parse(Console.ReadLine());
            Console.WriteLine();

            var serie= repositorio.RetornaPorId(indiceSerie);

            Console.WriteLine(serie);
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Series a seu dispor!");
            Console.WriteLine("Informe a ação desejada:");

            Console.WriteLine("1- Listar séries");
            Console.WriteLine("2- Inserir nova série");
            Console.WriteLine("3- Atualizar série");
            Console.WriteLine("4- Excluir série");
            Console.WriteLine("5- Visualizar série");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario= Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}
