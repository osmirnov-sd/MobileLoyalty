package com.example.mobileloyaltyapplication

import com.example.mobileloyaltyapplication.models.Ads
import com.example.mobileloyaltyapplication.models.History
import com.example.mobileloyaltyapplication.models.Profile
import com.squareup.moshi.Moshi
import com.squareup.moshi.kotlin.reflect.KotlinJsonAdapterFactory
import retrofit2.Call
import retrofit2.Retrofit
import retrofit2.converter.moshi.MoshiConverterFactory
import retrofit2.converter.scalars.ScalarsConverterFactory
import retrofit2.http.GET

private const val BASE_URL = "http://mobileloyaltyapi.gear.host/api/ui/"

//var gson = GsonBuilder()
//    .setLenient()
//    .create()

private val moshi = Moshi.Builder()
    .add(KotlinJsonAdapterFactory())
    .build()

private val retrofit = Retrofit.Builder()
    .addConverterFactory(MoshiConverterFactory.create(moshi))
    .baseUrl(BASE_URL)
    .build()

interface MobileLoyaltyApiService {
    @GET("profile")
    fun getProfile(): Call<Profile>

    @GET("ads")
    fun getAds() : Call<Ads>

    @GET("history")
    fun getHistory() : Call<History>
}

object MobileLoyaltyApi{
    val retrofitService : MobileLoyaltyApiService by lazy {
        retrofit.create(MobileLoyaltyApiService::class.java)
    }
}