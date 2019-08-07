package com.example.mobileloyaltyapplication.fragments

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import androidx.fragment.app.Fragment
import com.example.mobileloyaltyapplication.R
import com.squareup.picasso.Picasso

class QRFragment : Fragment() {

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {

        return inflater.inflate(R.layout.fragment_qr, container, false)
    }

    override fun onActivityCreated(savedInstanceState: Bundle?) {
        super.onActivityCreated(savedInstanceState)

        val qrImage : ImageView = getView()!!.findViewById(R.id.image_qr)
        var url : String = "http://mobileloyaltyapi.gear.host/api/ui/code"

        Picasso.with(getActivity()!!.applicationContext)
            .load(url)
            .resize(600,600)
            .into(qrImage)

    }
}