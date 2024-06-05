using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.Linq;

namespace gcgcg
{
    internal class VectorEditor : Objeto
    {
        char _rotuloInicial;
        private IList<char> rotulos;

        private Objeto _root = null;
        private Objeto _lastNode = null;

        public bool editing { get; set; }

        public VectorEditor(Objeto _paiRef, ref char _rotulo, Objeto objetoFilho = null) : base(_paiRef, ref _rotulo, objetoFilho)
        {
            PrimitivaTipo = PrimitiveType.Points;
            PrimitivaTamanho = 1;
            editing = false;
            _rotuloInicial = _rotulo;
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

            GrafocenaRemoverObjeto(poligon);
            Atualizar();
        }

        internal Objeto addPoligon(Objeto pai)
        {
            char rotulo = Utilitario.CharProximo(rotulos.Count == 0 ? _rotuloInicial : rotulos.Last());
            _lastNode = new Poligono(pai ?? this, ref rotulo, new List<Ponto4D>());
            rotulos.Add(rotulo);

            if (_root == null) _root = _lastNode;

            this.editing = true;
            base.FilhoAdicionar(_lastNode);

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
