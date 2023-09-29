using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi;

// Book sýnýfý, veritabanýndaki "Books" tablosuna karþýlýk gelir.
public class Book
{
    // Kitap ID'si, veritabaný tarafýndan otomatik olarak oluþturulur.
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    // Kitap baþlýðý, zorunlu bir alan olarak iþaretlenmiþtir.
    [Required]
    public string Title { get; set; }

    // Kitap türü ID'si, bu kitabýn türünü belirtir.
    public int GenreId { get; set; }

    // Kitap sayfa sayýsý.
    public int PageCount { get; set; }

    // Kitap yayýn tarihi.
    public DateTime PublishDate { get; set; }
}
