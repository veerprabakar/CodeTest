# CodeTest (Plexure)


Exercise 1:

* Created a Console application which shows length of Http content for given URLs.

Please note: The number resource URLs can be listed inside the Program.cs file. (restricted to less than three)


Exercise 3:

Coupon Manager test cases:

 
 [Test]
   public static void Test_Constructor_Invalid()
        {
            var logger = new FakeLogger();
            var couponProvider = new FakeCouponProvide();
                        
            Assert.Throws<ArgumentNullException>(() => 
                    new CouponManager(null, couponProvider));

            Assert.Throws<ArgumentNullException>(() => 
                    new CouponManager(logger, null));
        }


         [TestCase("ea6d2629-5498-4d3d-af37-29f23a31c94b", 
                        "4e8f0179-530b-441e-b991-64d2109f9963")]
        public static void Test_CanRedeemCoupon_Error_Conditions(Guid couponId, Guid userId)
        {
            var couponManager = new CouponManager();
            
            // Expecting arg null exception
            Assert.Throws<ArgumentNullException>(() => 
                    couponManager.CanRedeemCoupon(couponId, userId, null);
            
            // Send empty GUID - Expecting KeyNotFoundException
            Assert.Throws<KeyNotFoundException>(() => 
                    couponManager.CanRedeemCoupon(Guid.Empty, userId, null);

        }

        [TestCase("ea6d2629-5498-4d3d-af37-29f23a31c94b", 
                    "4e8f0179-530b-441e-b991-64d2109f9963")]
        public static void Test_CanRedeemCoupon_Fail_Conditions(Guid couponId, Guid userId)
        {
            var couponManager = new CouponManager();
            var evaluators = new Func<string, Guid, bool>[]
            {
                (c, g) => false,
                (c, g) => true,
                (c, g) => false
            };
            Assert.False(couponManager.CanRedeemCoupon(couponId, userId, evaluators));
        }

        [TestCase("ea6d2629-5498-4d3d-af37-29f23a31c94b", 
                    "4e8f0179-530b-441e-b991-64d2109f9963")]
        public static void Test_CanRedeemCoupon_Test_Success_Conditions(Guid couponId, Guid userId)
        {
            var couponManager = new CouponManager();
            var evaluators = new Func<string, Guid, bool>[]{};
            Assert.True(couponManager.CanRedeemCoupon(couponId, userId, evaluators));

            var evaluatorsAllTrue = new Func<string, Guid, bool>[]
            {
                (c, g) => true,
                (c, g) => true
            };
            Assert.True(couponManager.CanRedeemCoupon(couponId, userId, evaluatorsAllTrue));
        }
