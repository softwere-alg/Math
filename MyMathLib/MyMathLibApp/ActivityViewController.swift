//
//  ActivityViewController.swift
//  MyMathLibApp
//
//  Created by 田中祐汰 on 2024/09/04.
//

import Foundation
import UIKit

class ActivityViewController: UIViewController {

    private let activityView = ActivityView()

    init(message: String) {
        super.init(nibName: nil, bundle: nil)
        modalTransitionStyle = .crossDissolve
        modalPresentationStyle = .overFullScreen
        activityView.messageLabel.text = message
        view = activityView
    }

    required init(coder aDecoder: NSCoder) {
        fatalError("init(coder:) has not been implemented")
    }
}

private class ActivityView: UIView {

    let activityIndicatorView = UIActivityIndicatorView(style: .whiteLarge)
    let boundingBoxView = UIView(frame: CGRectZero)
    let messageLabel = UILabel(frame: CGRectZero)

    init() {
        super.init(frame: CGRectZero)

        backgroundColor = UIColor(white: 0.0, alpha: 0.5)

        boundingBoxView.backgroundColor = UIColor(white: 0.0, alpha: 0.5)
        boundingBoxView.layer.cornerRadius = 12.0

        activityIndicatorView.startAnimating()

        messageLabel.font = UIFont.boldSystemFont(ofSize: UIFont.labelFontSize)
        messageLabel.textColor = UIColor.white
        messageLabel.textAlignment = .center
        messageLabel.shadowColor = UIColor.black
        messageLabel.shadowOffset = CGSizeMake(0.0, 1.0)
        messageLabel.numberOfLines = 0

        addSubview(boundingBoxView)
        addSubview(activityIndicatorView)
        addSubview(messageLabel)
    }

    required init(coder aDecoder: NSCoder) {
        fatalError("init(coder:) has not been implemented")
    }

    override func layoutSubviews() {
        super.layoutSubviews()

        boundingBoxView.frame.size.width = 160.0
        boundingBoxView.frame.size.height = 160.0
        boundingBoxView.frame.origin.x = ceil((bounds.width / 2.0) - (boundingBoxView.frame.width / 2.0))
        boundingBoxView.frame.origin.y = ceil((bounds.height / 2.0) - (boundingBoxView.frame.height / 2.0))

        activityIndicatorView.frame.origin.x = ceil((bounds.width / 2.0) - (activityIndicatorView.frame.width / 2.0))
        activityIndicatorView.frame.origin.y = ceil((bounds.height / 2.0) - (activityIndicatorView.frame.height / 2.0))

        let messageLabelSize = messageLabel.sizeThatFits(CGSizeMake(160.0 - 20.0 * 2.0, CGFloat.greatestFiniteMagnitude))
        messageLabel.frame.size.width = messageLabelSize.width
        messageLabel.frame.size.height = messageLabelSize.height
        messageLabel.frame.origin.x = ceil((bounds.width / 2.0) - (messageLabel.frame.width / 2.0))
        messageLabel.frame.origin.y = ceil(activityIndicatorView.frame.origin.y + activityIndicatorView.frame.size.height + ((boundingBoxView.frame.height - activityIndicatorView.frame.height) / 4.0) - (messageLabel.frame.height / 2.0))
    }
}
