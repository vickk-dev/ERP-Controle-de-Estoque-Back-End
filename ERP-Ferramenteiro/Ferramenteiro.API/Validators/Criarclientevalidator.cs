//using Ferramenteiro.Application.DTOs;
//using FluentValidation;

//namespace Ferramenteiro.API.Validators;

//public class CriarClienteValidator : AbstractValidator<CriarClienteDto>
//{
//    private static readonly string[] TiposValidos = ["CPF", "CNPJ"];

//    public CriarClienteValidator()
//    {
//        RuleFor(x => x.TipoDocumento)
//            .NotEmpty().WithMessage("TipoDocumento é obrigatório.")
//            .Must(t => TiposValidos.Contains(t?.ToUpper()))
//            .WithMessage("TipoDocumento deve ser 'CPF' ou 'CNPJ'.");

//        RuleFor(x => x.Documento)
//            .NotEmpty().WithMessage("Documento é obrigatório.")
//            .When(x => x.TipoDocumento?.ToUpper() == "CPF", ApplyConditionTo.CurrentValidator)
//            .DependentRules(() =>
//            {
//                RuleFor(x => x.Documento)
//                    .Must((dto, doc) => ValidarCpfOuCnpj(dto.TipoDocumento, doc))
//                    .WithMessage("CPF deve ter 11 dígitos numéricos e CNPJ deve ter 14 dígitos numéricos.");
//            });

//        RuleFor(x => x.Documento)
//            .NotEmpty().WithMessage("Documento é obrigatório.")
//            .Matches(@"^\d+$").WithMessage("Documento deve conter apenas números.")
//            .Must((dto, doc) => ValidarCpfOuCnpj(dto.TipoDocumento, doc))
//            .WithMessage("CPF deve ter 11 dígitos e CNPJ deve ter 14 dígitos.");

//        RuleFor(x => x.NomeRazaoSocial)
//            .NotEmpty().WithMessage("NomeRazaoSocial é obrigatório.")
//            .MaximumLength(200).WithMessage("NomeRazaoSocial deve ter no máximo 200 caracteres.");

//        RuleFor(x => x.EnderecoCompleto)
//            .NotEmpty().WithMessage("EnderecoCompleto é obrigatório (necessário para logística de entrega).")
//            .MaximumLength(500).WithMessage("EnderecoCompleto deve ter no máximo 500 caracteres.");

//        RuleFor(x => x.Telefone)
//            .Matches(@"^\d{10,11}$").WithMessage("Telefone deve ter 10 ou 11 dígitos numéricos.")
//            .When(x => !string.IsNullOrWhiteSpace(x.Telefone));
//    }

//    private static bool ValidarCpfOuCnpj(string? tipoDocumento, string? documento)
//    {
//        if (string.IsNullOrWhiteSpace(documento)) return false;
//        var digitos = new string(documento.Where(char.IsDigit).ToArray());

//        return tipoDocumento?.ToUpper() switch
//        {
//            "CPF" => digitos.Length == 11,
//            "CNPJ" => digitos.Length == 14,
//            _ => false
//        };
//    }
//}