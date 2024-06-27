//https://github.com/mono/opentk/blob/main/Source/Examples/Shapes/Old/Cube.cs

#define CG_Debug
using CG_Biblioteca;
using OpenTK.Core.Native;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Collections.Generic;
using System.Drawing;

namespace gcgcg
{
    internal class Cubo : Objeto
    {
        Ponto4D[] vertices;
        public bool _bMenor = false;
        private List<Ponto4D> _orbita = [];

        public Cubo(Objeto _paiRef, ref char _rotulo, bool bMenor = false) : base(_paiRef, ref _rotulo)
        {
            PrimitivaTipo = PrimitiveType.Triangles;
            _bMenor = bMenor;
            PrimitivaTamanho = bMenor ? .3f : 1f;
            _shaderObjeto = new Shader("Shaders/shader.vert", "Shaders/shader.frag");

            if (bMenor)
            {
                for (int angulo = 0; angulo <= 360; angulo += 5)
                    _orbita.Add(Matematica.GerarPtosCirculo(angulo, 0.5));
            }

            vertices =
            [
                new Ponto4D(-1.0f, -1.0f,  1.0f + (bMenor ? 7f : 0f)), // 0
                new Ponto4D( 1.0f, -1.0f,  1.0f + (bMenor ? 7f : 0f)), // 1
                new Ponto4D( 1.0f,  1.0f,  1.0f + (bMenor ? 7f : 0f)), // 2
                new Ponto4D(-1.0f,  1.0f,  1.0f + (bMenor ? 7f : 0f)), // 3
                new Ponto4D(-1.0f, -1.0f, -1.0f + (bMenor ? 7f : 0f)), // 4
                new Ponto4D( 1.0f, -1.0f, -1.0f + (bMenor ? 7f : 0f)), // 5
                new Ponto4D( 1.0f,  1.0f, -1.0f + (bMenor ? 7f : 0f)), // 6
                new Ponto4D(-1.0f,  1.0f, -1.0f + (bMenor ? 7f : 0f)), // 7
            ];

            int[] indices =
            [
                0, 1, 2, 2, 3, 0,
                1, 5, 6, 6, 2, 1,
                2, 6, 7, 7, 3, 2,
                5, 4, 7, 7, 6, 5,
                4, 0, 3, 3, 7, 4,
                4, 5, 1, 1, 0, 4,
            ];

            _texture = Texture.LoadFromFile("../../../Resources/Grupo.png");
            _texture.Coords =
            [
                [1, 1], // 0
                [1, 0], // 1
                [0, 1], // 2
                [0, 0], // 3
            ];

            int[] textureIndices =
            [
                3, 1, 0, 0, 2, 3,
                3, 1, 0, 0, 2, 3,
                3, 1, 0, 0, 2, 3,
                3, 1, 0, 0, 2, 3,
                3, 1, 0, 0, 2, 3,
                3, 1, 0, 0, 2, 3,
            ];

            foreach (var idx in indices)
                PontosAdicionar(new (vertices[idx].X * PrimitivaTamanho, vertices[idx].Y * PrimitivaTamanho, vertices[idx].Z * PrimitivaTamanho));

            foreach (var idx in textureIndices)
                _texturePoints.Add(new Ponto4D(_texture.Coords[idx][0], _texture.Coords[idx][1]));

            Atualizar();
        }

        public override void AlterarPosicao()
        {
            if (_bMenor)
            {
                foreach (var ponto in pontosLista)
                {
                    foreach (var ptrOrbita in _orbita)
                    {
                        ponto.Y = ptrOrbita.X;
                        ponto.Z = ptrOrbita.Y;
                    }
                }
            }

            Atualizar();
        }

        private void Atualizar()
        {
            ObjetoAtualizar();
        }

#if CG_Debug
        public override string ToString()
        {
            string retorno;
            retorno = "__ Objeto Cubo _ Tipo: " + PrimitivaTipo + " _ Tamanho: " + PrimitivaTamanho + "\n";
            retorno += base.ImprimeToString();
            return (retorno);
        }
#endif

    }
}
