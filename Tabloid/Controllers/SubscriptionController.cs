﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using Tabloid.Data;
using Tabloid.Models;
using Tabloid.Repositories;

namespace Tabloid.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly SubscriptionRepository _subscriptionRepository;
        private readonly UserProfileRepository _userProfileRepository;

        public SubscriptionController(ApplicationDbContext context)
        {
            _subscriptionRepository = new SubscriptionRepository(context);
            _userProfileRepository = new UserProfileRepository(context);

        }

        [HttpGet]
        public IActionResult Get()
        {
            var currentUser = GetCurrentUserProfile();

            return Ok(_subscriptionRepository.GetBySubscriberProfileId(currentUser.Id));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_subscriptionRepository.GetSubscription(id));
        }

        [HttpPost]
        public IActionResult Post(Subscription subscription)
        {
            var currentUser = GetCurrentUserProfile();

            subscription.SubscriberUserProfileId = currentUser.Id;

            subscription.BeginDateTime = DateTime.Now;

            _subscriptionRepository.Add(subscription);

            return CreatedAtAction("Get", new { id = subscription.Id }, subscription);
        }
        [HttpPut("{id}")]
        public IActionResult Update(Subscription subscription)
        {
            subscription.EndDateTime = DateTime.Now;
            _subscriptionRepository.Update(subscription);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _subscriptionRepository.Delete(id);
            return NoContent();
        }
        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        }
    }
}
