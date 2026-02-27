# SimpleNotepad

Um editor de texto simples desenvolvido em C# utilizando Windows Forms e .NET 10.

## Funcionalidades

O projeto inclui as seguintes capacidades:
- Leitura e escrita de arquivos (.txt).
- Barra de status com contador de linha atual, total de caracteres e total de palavras.
- Atalhos de teclado customizados (Ctrl+S para salvar, Ctrl+O para abrir).
- Navegação rápida entre linhas via Ctrl + Setas (Cima/Baixo).
- Suporte ao uso da tecla Tab dentro do editor.
- Executável independente com ícone personalizado.

## Requisitos e Compilação

Para compilar o projeto a partir do código-fonte:

1. Ter o .NET 10 SDK instalado.
2. Executar o comando de publicação:
   dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true

O executável gerado incluirá todas as dependências necessárias para rodar em sistemas Windows x64.

---

## Créditos
Este projeto utiliza um ícone do Flaticon:
[Notepad icons created by Freepik - Flaticon](https://www.flaticon.com/free-icons/notepad)