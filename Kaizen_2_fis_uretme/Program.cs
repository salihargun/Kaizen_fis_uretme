using System.Text.Json;
using System.Text;
using Kaizen_2_fis_uretme;

public class Parser()
{
    public List<ResponseDto> ReadJson()
    {
        var path = "Z:/response.json";
        string json;

        using (StreamReader sr = new StreamReader(path, Encoding.UTF8, false))
        {
            json = sr.ReadToEnd();
        }

        List<ResponseDto> response = JsonSerializer.Deserialize<List<ResponseDto>>(json);
        return response;
    }
}

internal class Program
{

    private static void Main(string[] args)
    {
        Parser parser = new Parser();
        var response = parser.ReadJson();  //Json dosyasını okuyarak responseDto'ları elde ediyoruz.

        var words = response.Where(x => x.locale == null).ToList();  // İlk satırdaki tüm description'ı içeren satırı almamak için böyle bir sorgu uyguladık. 
        var rows = new List<RowDto>();    // Satırlarımızı tutmak için liste oluşturduk.
        foreach (var word in words)   // Tüm kelimeleri tek tek satırlara yerleştireceğiz.
        {
            int wordYCord = word.boundingPoly.vertices.OrderBy(x => x.y).FirstOrDefault().y; // Kelimenin en küçük y kordinatını alıyoruz.
            var row = rows.FirstOrDefault(x => Math.Abs(x.yCord - wordYCord) <= 15); // Eğer mevcut satırlar arasında kelimenin y kordinatı ile arasındaki fark 15 den küçük olan bir satır var ise onu kullanıyoruz.
            if (row == null) // Eğer geçerli bir satır yok ise kendimiz bu koordinata sahip yeni bir satır oluşturuyoruz.
            {
                row = new RowDto(wordYCord); // Satır için kelimenin en küçük y koordinatını kullanıyoruz.
                rows.Add(row);
            }
            //Satır belirlendi.

            row.Words.Add(word); // Satırımızı seçtikten sonra kelimeyi ilgili satıra ekliyoruz.
        }
        foreach (var row in rows)
        {
            foreach (var word in row.Words)
            {
                Console.Write(word.description + " ");
            }
            Console.WriteLine();
        }


    }
}