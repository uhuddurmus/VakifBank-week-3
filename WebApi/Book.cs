using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi;

// Book s�n�f�, veritaban�ndaki "Books" tablosuna kar��l�k gelir.
public class Book
{
    // Kitap ID'si, veritaban� taraf�ndan otomatik olarak olu�turulur.
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    // Kitap ba�l���, zorunlu bir alan olarak i�aretlenmi�tir.
    [Required]
    public string Title { get; set; }

    // Kitap t�r� ID'si, bu kitab�n t�r�n� belirtir.
    public int GenreId { get; set; }

    // Kitap sayfa say�s�.
    public int PageCount { get; set; }

    // Kitap yay�n tarihi.
    public DateTime PublishDate { get; set; }
}
