using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace gcgcg
{
    internal class VectorEditor : Objeto
    {
        char _rotulo;

        private Objeto _root = null;
        private Objeto _lastNode = null;

        public bool editing { get; set; }

        public VectorEditor(Objeto _paiRef, ref char _rotulo, Objeto objetoFilho = null) : base(_paiRef, ref _rotulo, objetoFilho)
        {
            PrimitivaTipo = PrimitiveType.Points;
            PrimitivaTamanho = 1;
            editing = false;

            this._rotulo = _rotulo;
            Atualizar();
        }

        internal Objeto addPoligon(Objeto pai)
        {
            _lastNode = new Poligono(pai ?? this, ref _rotulo, new List<Ponto4D>());
            if (_root == null)
            {
                _root = _lastNode;
            }

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
