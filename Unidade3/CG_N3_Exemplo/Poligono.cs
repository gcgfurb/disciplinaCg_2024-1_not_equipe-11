#define CG_Debug

using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.Linq;

namespace gcgcg
{
    internal class Poligono : Objeto
    {
        public Poligono(Objeto _paiRef, ref char _rotulo, List<Ponto4D> pontosPoligono) : base(_paiRef, ref _rotulo)
        {
            PrimitivaTipo = PrimitiveType.LineLoop;
            PrimitivaTamanho = 1;
            base.pontosLista = pontosPoligono;
            Atualizar();
        }

        public int GetIdxPontoMenorDistancia(Ponto4D sruPonto)
        {
            return pontosLista
                .Select((ponto, idx) => new { ponto, idx })
                .MinBy((objPonto) => Matematica.DistanciaQuadrado(objPonto.ponto, sruPonto)).idx;
        }

        private void Atualizar()
        {
            base.ObjetoAtualizar();
        }

#if CG_Debug
        public override string ToString()
        {
            string retorno;
            retorno = "__ Objeto Poligono _ Tipo: " + PrimitivaTipo + " _ Tamanho: " + PrimitivaTamanho + "\n";
            retorno += base.ImprimeToString();
            return retorno;
        }
#endif

    }
}
