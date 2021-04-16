using System;
using System.IO;
using System.Collections.Generic;
using Series.Interfaces;

namespace Series
{
    public class SerieRepositorio : IRepositorio<Serie>
    {

        private List<Serie> listaSerie= new List<Serie>();
        public void Atualiza(int id, Serie objeto)
        {
            listaSerie[id]= objeto;
            SalvaArquivo salvaArquivo= new SalvaArquivo();
            salvaArquivo.SalvaArq(listaSerie);
        }

        public void Exclui(int Id)
        {
            listaSerie[Id].Excluir();
            SalvaArquivo salvaArquivo= new SalvaArquivo();
            salvaArquivo.SalvaArq(listaSerie);
        }

        public void InsereDoArquivo(Serie objeto)
        {
            listaSerie.Add(objeto);
        }

        public void Insere(Serie objeto)
        {
            listaSerie.Add(objeto);
            SalvaArquivo salvaArquivo= new SalvaArquivo();
            salvaArquivo.SalvaArq(listaSerie);
        }

        public List<Serie> Lista()
        {
            return listaSerie;
        }

        public int ProximoId()
        {
            return listaSerie.Count;
        }

        public Serie RetornaPorId(int id)
        {
            return listaSerie[id];
        }
    }

    static class LeArqSeries
    {
        public static void MontaRepositorio()
        {
            string[] linhas= System.IO.File.ReadAllLines("Series.csv");

            if(linhas.Length > 0)
            {
                foreach(string line in linhas)
                {
                    string[] columns = line.Split(',');
                    
                    int entradaId= int.Parse(columns[0]);
                    int entradaGenero= int.Parse(columns[1]);
                    string entradaTitulo= columns[2];
                    int entradaAno= int.Parse(columns[3]);
                    string entradaDescricao= columns[4];
                    bool entradaExcluido= bool.Parse(columns[5]);

                    Serie novaSerie= new Serie(id: entradaId,
                                                genero: (Genero)entradaGenero,
                                                titulo: entradaTitulo,
                                                ano: entradaAno,
                                                descricao: entradaDescricao,
                                                excluido: entradaExcluido);

                    Program.repositorio.InsereDoArquivo(novaSerie);
                }
            }
        }
    }

    public class SalvaArquivo
    {

        public void SalvaArq(List<Serie> listaDeSeries)
        {
            List<string> list= new List<string>();
            Serie serie;

            for (int i=0; i<listaDeSeries.Count; i++)
            {
                serie= listaDeSeries[i];
                int id= serie.retornaId();
                int gen= (int)serie.retornaGenero();
                int ano= serie.retornaAno();
                string tit=serie.retornaTitulo();
                string descr= serie.retornaDescricao();
                bool excl= serie.retornaExcluido();

                var newLine = string.Format("{0},{1},{2},{3},{4},{5}", id,gen,tit,ano, descr,excl);
                list.Add(newLine); 
            }
            File.WriteAllLines("Series.csv", list);
        }
    }
}