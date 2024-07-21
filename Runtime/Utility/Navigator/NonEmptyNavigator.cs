namespace Tactile.Core.Utility.Navigator
{
    public class NonEmptyNavigator<T>: Navigator<T>
    {
        public NonEmptyNavigator(T firstItem) : base(firstItem)
        {
        }
    }
}