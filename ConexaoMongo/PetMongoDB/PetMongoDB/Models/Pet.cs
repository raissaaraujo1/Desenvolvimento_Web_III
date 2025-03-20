namespace PetMongoDB.Models
{
    public class Pet
    {
        //usando um tipo Guid usado para chave que tenha números e letras 
        public Guid Id { get; set; }
        public string ? Nome { get; set; }
        public int Idade { get; set; }
        public string? Raca { get; set; }
        //Inserindo as info do cuidador do pet
        public string? Cuidador { get; set; }
        public string? Celular { get; set; }


    }
}
