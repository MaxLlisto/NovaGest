using System.Windows.Forms.Design.Behavior;
using System.Drawing;

namespace AwesomeShapeControl
{
    class ShapeGlyphBehavior : Behavior
    {

        #region Fields

        private Point _dragStart = Point.Empty;
        private IShape _shape;
        private int _pointIdx;
        private bool _dragging = false;


        #endregion

        #region Properties



        #endregion

        #region Construction / Deconstruction

        public ShapeGlyphBehavior(IShape shape, int pointIdx)
        {
            _shape = shape;
            _pointIdx = pointIdx;
        }

        #endregion

        #region Public Methods

        public override bool OnMouseDown(Glyph g, System.Windows.Forms.MouseButtons button, Point mouseLoc)
        {
            if ((button & System.Windows.Forms.MouseButtons.Left) == System.Windows.Forms.MouseButtons.Left)
            {
                _dragStart = mouseLoc;
                _dragging = true;
            }
            return true;
        }

        public override bool OnMouseUp(Glyph g, System.Windows.Forms.MouseButtons button)
        {
            if ((button & System.Windows.Forms.MouseButtons.Left) == System.Windows.Forms.MouseButtons.Left)
            {
                _dragging = false;
            }
            return true;
        }

        public override bool OnMouseMove(Glyph g, System.Windows.Forms.MouseButtons button, Point mouseLoc)
        {
            if (_dragging)
            {
                int xDiff = mouseLoc.X - _dragStart.X;
                int yDiff = mouseLoc.Y - _dragStart.Y;

                Point p = _shape.GetPoint(_pointIdx);

                if (xDiff == 0 && yDiff == 0)
                    return true;

                p.X += xDiff;
                p.Y += yDiff;
                _dragStart = mouseLoc;

                _shape.SetPoint(_pointIdx, p);
            }

            return true;
        }

        public override bool OnMouseLeave(Glyph g)
        {
            _dragging = false;
            return true;
        }

        #endregion

        #region Private Methods



        #endregion

    }
}
