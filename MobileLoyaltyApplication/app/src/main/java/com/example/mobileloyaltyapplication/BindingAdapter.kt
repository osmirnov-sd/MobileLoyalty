package com.example.mobileloyaltyapplication

import android.widget.ImageView
import androidx.core.net.toUri
import com.bumptech.glide.Glide

fun bindImage(imgView: ImageView, imgUrl: String?) {
    imgUrl?.let {
        val imgUri = imgUrl.toUri().buildUpon().scheme("https").build()
        Glide.with(imgView.context)
            .load(imgUri)
            .into(imgView)
    }
}