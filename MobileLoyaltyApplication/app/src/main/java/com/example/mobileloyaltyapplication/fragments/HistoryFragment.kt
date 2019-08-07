package com.example.mobileloyaltyapplication.fragments

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.fragment.app.Fragment
import com.example.mobileloyaltyapplication.MobileLoyaltyApi
import com.example.mobileloyaltyapplication.R
import com.example.mobileloyaltyapplication.models.History
import com.example.mobileloyaltyapplication.models.Profile
import org.w3c.dom.Text
import retrofit2.Call
import retrofit2.Response

class HistoryFragment : Fragment() {

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        return inflater.inflate(R.layout.fragment_history, container, false)
    }

    override fun onActivityCreated(savedInstanceState: Bundle?) {
        super.onActivityCreated(savedInstanceState)

        val history : TextView = getView()!!.findViewById(R.id.history_edit)

        MobileLoyaltyApi.retrofitService.getHistory().enqueue(object: retrofit2.Callback<History>{
            override fun onFailure(call: Call<History>, t: Throwable) {
                history.text = "Failure: " + t.message.toString()
            }

            override fun onResponse(call: Call<History>, response: Response<History>) {
                history.text = response.body()!!.History.joinToString()
            }

        })
    }
}
