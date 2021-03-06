﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tabloid.Data;
using Tabloid.Models;

namespace Tabloid.Repositories
{
    public class ReactionRepository
    {

        private readonly ApplicationDbContext _context;

        public ReactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Reaction> GetAll()
        {
            return _context.Reaction
                .Include(r => r.Emoji)
                .ToList();
        }

        public List<Emoji> GetAllEmojis()
        {
            return _context.Emoji
                .ToList();
        }

        public Reaction GetById(int id)
        {
            return _context.Reaction
                                .Include(r => r.Emoji)

               .FirstOrDefault(r => r.Id == id);
        }

        public void Add(Reaction Reaction)
        {
            _context.Add(Reaction);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var reaction = GetById(id);
            foreach (var postReaction in _context.PostReaction
                .Where(pr => pr.ReactionId == reaction.Id))
            {
                _context.PostReaction.Remove(postReaction);
            }

            _context.Reaction.Remove(reaction);
            _context.SaveChanges();
        }
        public void Update(Reaction reaction)
        {
            _context.Entry(reaction).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
