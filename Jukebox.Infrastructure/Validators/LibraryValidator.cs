namespace Jukebox.Infrastructure.Validators
{
    public class LibraryValidator : ILibraryValidator
    {
        public bool Valid { get; private set; }

        public void Invalidate()
        {
            Valid = false;
        }

        public void Validate()
        {
            Valid = true;
        }
    }
}
