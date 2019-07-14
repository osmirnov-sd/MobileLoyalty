package com.example.mobileloyaltyapp

import retrofit2.Call
import retrofit2.Retrofit
import retrofit2.converter.scalars.ScalarsConverterFactory
import retrofit2.http.GET

private const val BASE_URL = "https://mobileloyaltyapi.gear.host/api/"


private val retrofit = Retrofit.Builder()
    .addConverterFactory(ScalarsConverterFactory.create())
    .baseUrl(BASE_URL)
    .build()

interface MobileLoyaltyApiService {
    @GET("values")
    fun getValues(): Call<String>

}

object MobileLoyaltyApi{
    val retrofitService : MobileLoyaltyApiService by lazy {
        retrofit.create(MobileLoyaltyApiService::class.java)
    }
}