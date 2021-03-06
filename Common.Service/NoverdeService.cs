﻿using Common.Domain;
using Common.Util;
using Newtonsoft.Json;
using System;

namespace Common.Service
{
    public class NoverdeService
    {
        public static int GetScore(string cpf)
        {
			try
			{
				object data = new { CPF = cpf };

				string ret = RestService.Send(CommonHelper.UrlScore, CommonHelper.NoverdeToken, data, RestSharp.Method.POST);

				Score score = JsonConvert.DeserializeObject<Score>(ret);

				return score.Points;
			}
			catch (Exception ex)
			{
				throw new Exception($"Não foi possível obter score para o CPF {cpf}. Error: {ex}");
			}
        }

		public static decimal GetCommitment(string cpf)
		{
			try
			{
				object data = new { CPF = cpf };

				string ret = RestService.Send(CommonHelper.UrlCommitment, CommonHelper.NoverdeToken, data, RestSharp.Method.POST);

				Commitment commitment = JsonConvert.DeserializeObject<Commitment>(ret);

				return commitment.Percentage;
			}
			catch (Exception ex)
			{
				throw new Exception($"Não foi possível obter o comprometimento de renda para o CPF {cpf}. Error: {ex}");
			}
		}
	}
}
