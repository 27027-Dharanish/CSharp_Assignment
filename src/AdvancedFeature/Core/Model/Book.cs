namespace AdvancedFeature.Core.Model
{
    /// <summary>
    /// Represents a book entity with title, author, and unique identification properties.
    /// </summary>
    public record Book
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class.
        /// </summary>
        /// <param name="title">Title of the book.</param>
        /// <param name="author">Author name of the book.</param>
        /// <param name="iSBN">International Standard Book Number.</param>
        public Book(string title, string author, int iSBN)
        {
            this.Title = title;
            this.Author = author;
            this.ISBN = iSBN;
        }

        /// <summary>
        /// Gets the title of the book.
        /// </summary>
        /// <value>
        /// The title of the book.
        /// </value>
        public string Title { get; init; } = string.Empty;

        /// <summary>
        /// Gets or sets the author of the book.
        /// </summary>
        /// <value>
        /// The author of the book.
        /// </value>
        public string Author { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the International Standard Book Number.
        /// </summary>
        /// <value>
        /// The International Standard Book Number (ISBN).
        /// </value>
        public int ISBN { get; set; }

        /// <summary>
        /// The deconstruction method used to get the book details.
        /// </summary>
        /// <param name="title">Title of the book.</param>
        /// <param name="author">Author name of the book.</param>
        /// <param name="isbn">International Standard Book Number.</param>
        public void Deconstruct(out string title, out string author, out int isbn)
        {
            title = this.Title;
            author = this.Author;
            isbn = this.ISBN;
        }

    }
}
