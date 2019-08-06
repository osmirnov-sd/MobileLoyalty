package com.example.mobileloyaltyapplication.fragments

import android.os.Bundle
import android.view.LayoutInflater
import android.view.TextureView
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.fragment.app.Fragment
import com.example.mobileloyaltyapplication.MobileLoyaltyApi
import com.example.mobileloyaltyapplication.R
import com.example.mobileloyaltyapplication.models.Ads
import com.example.mobileloyaltyapplication.models.Profile
import retrofit2.Call
import retrofit2.Response

class AdsFragment : Fragment() {

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        return inflater.inflate(R.layout.fragment_ads, container, false)
    }

    override fun onActivityCreated(savedInstanceState: Bundle?) {
        super.onActivityCreated(savedInstanceState)

        val ads : TextView = getView()!!.findViewById(R.id.ads_edit)

        MobileLoyaltyApi.retrofitService.getAds().enqueue(object: retrofit2.Callback<Ads>{
            override fun onFailure(call: Call<Ads>, t: Throwable) {
                ads.text = "Failure: " + t.message.toString()
            }

            override fun onResponse(call: Call<Ads>, response: Response<Ads>) {
                ads.text = response.body()!!.Ads.joinToString()
            }

        })
    }
}