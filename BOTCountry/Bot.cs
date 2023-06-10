using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;
using System;
using Telegram.Bot.Types.ReplyMarkups;
using BOT;
using System.Linq;
using System.Collections.Generic;

namespace BOT
{
    public class CountryInfoBot
    {
        private readonly HttpClient _httpClient;
        private readonly TelegramBotClient _botClient;
        ReceiverOptions _receiverOptions = new ReceiverOptions();
        shortNames shortNames = new shortNames();


        public CountryInfoBot(string botToken)
        {
            _httpClient = new HttpClient();
            _botClient = new TelegramBotClient(botToken);
        }

        public async Task Start()
        {
            var me = await _botClient.GetMeAsync();
            Console.WriteLine($"Bot {me.Username} started.");

            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            _botClient.StartReceiving(HandlerUpdateAsync, HandlerErrorAsync, _receiverOptions, cancellationToken);

            Console.ReadKey();
            cancellationTokenSource.Cancel();
        }

        private Task HandlerErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"API error {apiRequestException.ErrorCode}: {apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage); 

            return Task.CompletedTask;
        }

        private async Task HandlerUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update.Message.Text != null)
            {
                await HandlerMessageAsync(botClient, update.Message);
            }
        }

        private async Task HandlerMessageAsync(ITelegramBotClient botClient, Message message)
        {
            if(message.Text == "/help")
            {
                botClient.SendTextMessageAsync(message.Chat.Id, "Hello! If you want to go in trip - i give you base information about all countries");
            }
            else if (shortNames.countryShortNames.Contains(message.Text) && message.Text != "ru")
            {
                var apiUrl = $"https://localhost:7241/api/Wikipedia/getcountry?newQuestion={message.Text}";

                var response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var countryInfoList = JsonConvert.DeserializeObject<List<CountryInfo>>(jsonResponse);

                if (countryInfoList != null && countryInfoList.Any())
                {
                    var countryName = countryInfoList[0].name.official;
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Country name: {countryInfoList[0].name.official}" + 
                        "\n" + $"Country area - {countryInfoList[0].area}" 
                        + "\n" + $"Country population - {countryInfoList[0].population}"
                        + "\n" + $"Country time - {string.Join(", ", countryInfoList[0].timezones)}"
                        + "\n" + $"Country capital - {string.Join("," ,countryInfoList[0].capital)}"
                        + "\n" + $"Country car side - {countryInfoList[0].car.side}"
                        + "\n" + $"Country landlocked? - {countryInfoList[0].landlocked}"
                        + "\n" + $"Continents - {string.Join("," ,countryInfoList[0].continents)}"
                        + "\n" + $"Start of week - {countryInfoList[0].startOfWeek}"
                        + "\n" + $"Country flag - {countryInfoList[0].flags.alt}`"
                        + "\n" + $"Country flag link - {countryInfoList[0].flags.png}"
                        + "\n" + $"Country borders - {string.Join(", " ,countryInfoList[0].borders)}"
                        + "\n" + $"Country fifa team - {countryInfoList[0].fifa}");
                }
                else
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Failed to retrieve country data.");
                }
            }
            else if (message.Text == "ru")
            {
                botClient.SendTextMessageAsync(message.Chat.Id, "Russia is a terrorist country. In this regard, information on it in this chat will never be available. Glory to Ukraine");
            }
            else { botClient.SendTextMessageAsync(message.Chat.Id, "Country with this name does not exist"); }
        }
    }
}