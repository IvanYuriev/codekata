﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kata1Supermarket
{
    public class SupermarketPricing
    {
        /// <summary>
        /// Basic description: http://codekata.com/kata/kata01-supermarket-pricing/
        /// This kata arose from some discussions we’ve been having at the DFW Practioners meetings. 
        /// The problem domain is something seemingly simple: pricing goods at supermarkets.
        /// Some things in supermarkets have simple prices: this can of beans costs $0.65. 
        /// Other things have more complex prices. For example:
        /// - three for a dollar (so what’s the price if I buy 4, or 5?)
        /// - $1.99/pound(so what does 4 ounces cost?)
        /// - buy two, get one free(so does the third item have a price?)
        ///This kata involves no coding.The exercise is to experiment with various models for 
        ///representing money and prices that are flexible enough to deal with these(and other) 
        ///pricing schemes, and at the same time are generally usable(at the checkout, 
        ///for stock management, order entry, and so on). Spend time considering issues such as:
        /// - does fractional money exist?
        /// - when(if ever) does rounding take place?
        /// - how do you keep an audit trail of pricing decisions(and do you need to)?
        /// - are costs and prices the same class of thing?
        /// - if a shelf of 100 cans is priced using “buy two, get one free”, how do you value the stock?
        ///This is an ideal shower-time kata, but be careful. Some of the problems are more subtle 
        ///than they first appear. I suggest that it might take a couple of weeks worth of showers 
        ///to exhaust the main alternatives.
        /// </summary>

        /// THOUGHTS:
        /// The strategy pattern is the first that came into my head.
        /// and may be a builder to construct\parse complex expressions
        /// bridge - is another way to represent separation of abstraction from implementation
        ///    Ex: new Line(x, y, new SolidPen(Color.Red), new ImageBrush('image.jpg'));
        /// Visitor ???

    }
}
