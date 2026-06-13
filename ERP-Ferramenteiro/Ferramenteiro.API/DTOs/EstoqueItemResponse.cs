namespace Ferramenteiro.Application.DTOs;

// Usamos record para respostas imutáveis
public record EstoqueItemResponse(
    string Id, 
    string Nome,
    string Marca,
    int Estoque,
    decimal PrecoDia
);