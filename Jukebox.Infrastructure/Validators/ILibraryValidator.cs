namespace Jukebox.Infrastructure.Validators
{
    public interface ILibraryValidator
    {
        bool Valid { get; }
        void Invalidate();
        void Validate();
    }
}