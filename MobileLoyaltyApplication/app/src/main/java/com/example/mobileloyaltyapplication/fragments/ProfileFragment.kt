package com.example.mobileloyaltyapplication.fragments

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.fragment.app.Fragment
import com.example.mobileloyaltyapplication.MobileLoyaltyApi
import com.example.mobileloyaltyapplication.R
import com.example.mobileloyaltyapplication.models.Profile
import retrofit2.Call
import retrofit2.Response

class ProfileFragment : Fragment() {

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {

        val fp = inflater.inflate(R.layout.fragment_profile, container, false)
        return fp
    }

    override fun onActivityCreated(savedInstanceState: Bundle?) {
        super.onActivityCreated(savedInstanceState)

        val username : TextView = getView()!!.findViewById(R.id.ads_edit)
        val balance : TextView = getView()!!.findViewById(R.id.balance_edit)
        val transaction : TextView = getView()!!.findViewById(R.id.transactions_edit)

                MobileLoyaltyApi.retrofitService.getProfile().enqueue(object: retrofit2.Callback<Profile>{
            override fun onFailure(call: Call<Profile>, t: Throwable) {
                username.text = "Failure: " + t.message.toString()
            }

            override fun onResponse(call: Call<Profile>, response: Response<Profile>) {
                username.text = response.body()!!.UserName
            }

        })

        MobileLoyaltyApi.retrofitService.getProfile().enqueue(object: retrofit2.Callback<Profile>{
            override fun onFailure(call: Call<Profile>, t: Throwable) {
                balance.text = "Failure: " + t.message.toString()
            }

            override fun onResponse(call: Call<Profile>, response: Response<Profile>) {
                balance.text = response.body()!!.Balance.toString()
            }

        })

        MobileLoyaltyApi.retrofitService.getProfile().enqueue(object: retrofit2.Callback<Profile>{
            override fun onFailure(call: Call<Profile>, t: Throwable) {
                transaction.text = "Failure: " + t.message.toString()
            }

            override fun onResponse(call: Call<Profile>, response: Response<Profile>) {
                transaction.text = response.body()!!.Transactions.joinToString()
            }

        })
    }
}