public class Rectangle
{
    public float centerX;
    public float centerY;
    public float width;
    public float height;

    public Rectangle(float x, float y, float w, float h)
    {
        centerX = x;
        centerY = y;
        width = w;
        height = h;
    }

    public bool Contains(Point point)
    {
        bool contains = (point.x > centerX - width && point.x < centerX + width && point.y > centerY - height && point.y < centerY + height);
        return contains;
    }
}
