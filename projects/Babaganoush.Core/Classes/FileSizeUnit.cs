namespace Babaganoush.Core.Classes
{
    /// <summary>
    /// A representation of various units used when measuring the size of files.
    /// </summary>
    public sealed class FileSizeUnit
    {
        /// <summary>
        /// KiloByte file size unit.
        /// </summary>
        public static readonly FileSizeUnit KiloByte = new FileSizeUnit(0x400, "KB");

        /// <summary>
        /// MegaByte file size unit.
        /// </summary>
        public static readonly FileSizeUnit MegaByte = new FileSizeUnit(0x100000, "MB");

        /// <summary>
        /// GigaByte file size unit.
        /// </summary>
        public static readonly FileSizeUnit GigaByte = new FileSizeUnit(0x40000000, "GB");

        /// <summary>
        /// TeraByte file size unit.
        /// </summary>
        public static readonly FileSizeUnit TeraByte = new FileSizeUnit(0x10000000000, "TB");

        /// <summary>
        /// PetaByte file size unit.
        /// </summary>
        public static readonly FileSizeUnit PetaByte = new FileSizeUnit(0x4000000000000, "PB");

        /// <summary>
        /// ExaByte file size unit.
        /// </summary>
        public static readonly FileSizeUnit ExaByte = new FileSizeUnit(0x1000000000000000, "EB");

        /// <summary>
        /// The size, in bytes, of the given FileSizeUnit.
        /// </summary>
        public readonly ulong Size;

        /// <summary>
        /// The suffix of the given FileSizeUnit.
        /// </summary>
        public readonly string Suffix;

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="size">The size, in bytes, of the given FileSizeUnit.</param>
        /// <param name="suffix">The suffix of the given FileSizeUnit.</param>
        private FileSizeUnit(ulong size, string suffix)
        {
            Size = size;
            Suffix = suffix;
        }
    }
}