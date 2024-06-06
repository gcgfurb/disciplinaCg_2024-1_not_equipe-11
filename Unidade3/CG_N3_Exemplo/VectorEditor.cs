using System.Collections.Generic;
using System.Linq;
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
    internal class VectorEditor : Objeto
    {
        private List<char> rotulos;

        private Objeto _root = null;
        private Objeto _lastNode = null;

        public bool editing { get; set; }

        public VectorEditor(Objeto _paiRef, ref char _rotulo, Objeto objetoFilho = null) : base(_paiRef, ref _rotulo,
            objetoFilho)
        {
            PrimitivaTipo = PrimitiveType.Points;
            PrimitivaTamanho = 1;
            editing = false;
            rotulos = new List<char>();

            Atualizar();
        }

        internal Objeto getPoligonbyClick(Ponto4D mousePonto)
        {
            if (_root == null) return null;

            foreach (char rotulo in rotulos)
            {
                Objeto poligon = GrafocenaBusca(rotulo);
                if (poligon.Bbox().Dentro(mousePonto)) return poligon;
            }

            return null;
        }

        internal void deletePoligon(Objeto poligon)
        {
            if (_root == null || poligon == null) return;


            IList<Objeto> poligonosRemover = new List<Objeto>() { poligon };

            foreach (char rotulo in rotulos)
            {
                var poli = GrafocenaBusca(rotulo);
                if (poligonosRemover.Contains(poli.paiRef))
                    poligonosRemover.Add(poli);
            }

            rotulos.RemoveAll(x => poligonosRemover.Select(y => y.Rotulo).Contains(x));
            GrafocenaRemoverObjeto(poligon);
            Atualizar();
        }

        internal Objeto addPoligon(Poligono poligono, char rotulo)
        {
            _lastNode = poligono;
            rotulos.Add(rotulo);

            if (_root == null) _root = _lastNode;

            this.editing = true;

            return _lastNode;
        }

        internal Objeto finalizePoligon()
        {
            editing = false;
            return _lastNode;
        }

        internal void addNewPoligonPoint(Ponto4D mousePonto)
        {
            _lastNode.PontosAdicionar(mousePonto);
            Atualizar();
        }

        private void Atualizar()
        {
            base.ObjetoAtualizar();
        }
    }
}