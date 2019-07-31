package com.example.mobileloyaltyapplication

import retrofit2.Call
import retrofit2.Retrofit
import retrofit2.converter.scalars.ScalarsConverterFactory
import retrofit2.http.GET

private const val BASE_URL = "http://mobileloyaltyapi.gear.host/api/ui/profile"


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