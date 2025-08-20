public class FuneralPlansDTO
{
    public int Id { get; set; }

    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        set => _name = value?.Trim() ?? string.Empty;
    }

    public string Description { get; set; } = string.Empty;
    public double AnnualValue { get; set; }
    public bool Available { get; set; }
    public int MaxDependents { get; set; }
    public int MaxAge { get; set; }
    public double DependentAdditional { get; set; }

    public static void Validate(FuneralPlansDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "O objeto FuneralPlansDTO não pode ser nulo.");

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("O nome do plano funerário é obrigatório.");

        if (string.IsNullOrWhiteSpace(dto.Description))
            throw new ArgumentException("A descrição do plano funerário é obrigatória.");

        if (dto.AnnualValue < 0)
            throw new ArgumentException("O valor anual não pode ser negativo.");

        if (dto.MaxDependents < 0)
            throw new ArgumentException("O número máximo de dependentes não pode ser negativo.");

        if (dto.MaxAge < 0)
            throw new ArgumentException("A idade máxima não pode ser negativa.");

        if (dto.DependentAdditional < 0)
            throw new ArgumentException("O valor adicional por dependente não pode ser negativo.");
    }
}
