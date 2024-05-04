# CA_RS10_ParkNetProject
Project II | C# ASP.NET | Parking Lot Software | Grade 9.89

![Park Net (1)](https://github.com/fabiojspinheiro/CA_RS10_ParkNetProject_v2/assets/151678610/07d8cce2-4fb1-425a-ba5b-aad7a3f73a16)

# Problema
Necessidade de implementar um sistema integrado de gestão de todos os parques de estacionamento de uma empresa.
Os parques são geralmente operados em edifícios com vários pisos, e podem ser usados por
veículos ligeiros e motos.
Há também a possibilidade de comprar uma avença mensal, trimestral, semestral ou anual.
Nestes casos, o utilizador pode estacionar a viatura 24h por dia sem qualquer outro custo
adicional, com a vantagem de ter sempre um local reservado durante o período válido da
avença.
O pagamento é feito por carregamento através de cartão bancário previamente registado,
ficando esse valor em saldo até à sua total utilização ou levantamento

# Pré-requisitos
Antes de executar a aplicação é necessário fazer a criação da base de dados através da “Package Manager Console” do Microsoft Visual Studio, utilizando por definição o projeto "ParkNet_FabioPinheiro.App", através do comando “update-database”.

# Outros Requisitos 
É necessário garantir que o Admin previamente registou:
  - Pelo menos um parque de estacionamento;
  - Todos os preçários relativamente as avenças e tickets;
    
O preçário dos tickets deverá ser inserido desde os 00 minutos até ao período máximo estipulado, exemplo:
  - Intervalo de 15 minutos: 			Start 00:00 – End 00:15
  - Intervalo de 30 minutos:			Start 00:00 – End 00:30
  - Intervalo de 1 hora:				Start 00:00 – End 01:00
  
Os dados de acesso do admin encontram-se no ficheiro "appsettings.json".

