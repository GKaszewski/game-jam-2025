using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Data
{
    [Serializable]
    public class CharacterAttributes
    {
        [OdinSerialize] public float health = 100f;
        [OdinSerialize] public float maxHealth = 100f;
        [OdinSerialize] public float moveSpeed = 5f;
        [OdinSerialize] public float luck = 0f;
        [OdinSerialize] public float armor = 0f;
        [OdinSerialize] public int level = 1;
        [OdinSerialize] public int experience = 0;

        [OdinSerialize, PropertyTooltip("This is damage multiplier")]
        public float damage = 1f;

        [OdinSerialize, PropertyTooltip("This is damage multiplier for ranged attacks")]
        public float rangedDamage = 1f;

        [OdinSerialize, PropertyTooltip("This is damage multiplier for melee attacks")]
        public float meleeDamage = 1f;

        [OdinSerialize] public float attackRange = 16f;
        [OdinSerialize] public float attackSpeed = 1f;

        public event Action<float> OnHealthChanged;
        public event Action<float> OnMaxHealthChanged;
        public event Action<float> OnMoveSpeedChanged;
        public event Action<float> OnLuckChanged;
        public event Action<float> OnArmorChanged;
        public event Action<float> OnDamageChanged;
        public event Action<float> OnRangedDamageChanged;
        public event Action<float> OnMeleeDamageChanged;
        public event Action<float> OnAttackRangeChanged;
        public event Action<float> OnAttackSpeedChanged;
        
        public event Action<int> OnExperienceChanged;
        public event Action<int> OnLevelChanged;

        public float Health
        {
            get => health;
            private set
            {
                if (Math.Abs(health - value) < float.Epsilon) return;
                health = value;
                OnHealthChanged?.Invoke(health);
            }
        }

        public float MaxHealth
        {
            get => maxHealth;
            private set
            {
                if (Math.Abs(maxHealth - value) < float.Epsilon) return;
                maxHealth = value;
                OnMaxHealthChanged?.Invoke(maxHealth);
            }
        }

        public float MoveSpeed
        {
            get => moveSpeed;
            private set
            {
                if (Math.Abs(moveSpeed - value) < float.Epsilon) return;
                moveSpeed = value;
                OnMoveSpeedChanged?.Invoke(moveSpeed);
            }
        }

        public float Luck
        {
            get => luck;
            private set
            {
                if (Math.Abs(luck - value) < float.Epsilon) return;
                luck = value;
                OnLuckChanged?.Invoke(luck);
            }
        }

        public float Armor
        {
            get => armor;
            private set
            {
                if (Math.Abs(armor - value) < float.Epsilon) return;
                armor = value;
                OnArmorChanged?.Invoke(armor);
            }
        }

        public float Damage
        {
            get => damage;
            private set
            {
                if (Math.Abs(damage - value) < float.Epsilon) return;
                damage = value;
                OnDamageChanged?.Invoke(damage);
            }
        }

        public float RangedDamage
        {
            get => rangedDamage;
            private set
            {
                if (Math.Abs(rangedDamage - value) < float.Epsilon) return;
                rangedDamage = value;
                OnRangedDamageChanged?.Invoke(rangedDamage);
            }
        }

        public float MeleeDamage
        {
            get => meleeDamage;
            private set
            {
                if (Math.Abs(meleeDamage - value) < float.Epsilon) return;
                meleeDamage = value;
                OnMeleeDamageChanged?.Invoke(meleeDamage);
            }
        }

        public float AttackRange
        {
            get => attackRange;
            private set
            {
                if (Math.Abs(attackRange - value) < float.Epsilon) return;
                attackRange = value;
                OnAttackRangeChanged?.Invoke(attackRange);
            }
        }

        public float AttackSpeed
        {
            get => attackSpeed;
            private set
            {
                if (Math.Abs(attackSpeed - value) < float.Epsilon) return;
                attackSpeed = value;
                OnAttackSpeedChanged?.Invoke(attackSpeed);
            }
        }
        
        public int Experience
        {
            get => experience;
            private set
            {
                if (experience == value) return;
                experience = value;
                OnExperienceChanged?.Invoke(experience);
                
                //TODO: Implement level up logic
            }
        }
        
        public int Level
        {
            get => level;
            private set
            {
                if (level == value) return;
                level = value;
                OnLevelChanged?.Invoke(level);
            }
        }

        public void SetHealth(float value)
        {
            Health = Math.Clamp(value, 0, MaxHealth);
        }

        public void SetMaxHealth(float value)
        {
            MaxHealth = Math.Max(value, 0);
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }

        public void SetMoveSpeed(float value)
        {
            MoveSpeed = Math.Max(value, 0);
        }

        public void SetLuck(float value)
        {
            Luck = Math.Max(value, 0);
        }

        public void SetArmor(float value)
        {
            Armor = Math.Max(value, 0);
        }

        public void SetDamage(float value)
        {
            Damage = Math.Max(value, 0);
        }

        public void SetRangedDamage(float value)
        {
            RangedDamage = Math.Max(value, 0);
        }

        public void SetMeleeDamage(float value)
        {
            MeleeDamage = Math.Max(value, 0);
        }

        public void SetAttackRange(float value)
        {
            AttackRange = Math.Max(value, 0);
        }

        public void SetAttackSpeed(float value)
        {
            AttackSpeed = Math.Max(value, 0);
        }
    
        public void SetExperience(int value)
        {
            Experience = Math.Max(value, 0);
        }
        
        public void SetLevel(int value)
        {
            Level = Math.Max(value, 1);
        }
        
        public void ModifyHealth(float delta)
        {
            SetHealth(Health + delta);
        }

        public void ModifyMaxHealth(float delta)
        {
            SetMaxHealth(MaxHealth + delta);
        }

        public void ModifyMoveSpeed(float delta)
        {
            SetMoveSpeed(MoveSpeed + delta);
        }

        public void ModifyLuck(float delta)
        {
            SetLuck(Luck + delta);
        }

        public void ModifyArmor(float delta)
        {
            SetArmor(Armor + delta);
        }

        public void ModifyDamage(float delta)
        {
            SetDamage(Damage + delta);
        }

        public void ModifyRangedDamage(float delta)
        {
            SetRangedDamage(RangedDamage + delta);
        }

        public void ModifyMeleeDamage(float delta)
        {
            SetMeleeDamage(MeleeDamage + delta);
        }

        public void ModifyAttackRange(float delta)
        {
            SetAttackRange(AttackRange + delta);
        }

        public void ModifyAttackSpeed(float delta)
        {
            SetAttackSpeed(AttackSpeed + delta);
        }
        
        public void ModifyExperience(int delta)
        {
            SetExperience(Experience + delta);
        }
        
        public void ModifyLevel(int delta)
        {
            SetLevel(Level + delta);
        }

        public void Reset()
        {
            Health = MaxHealth = 100f;
            MoveSpeed = 5f;
            Luck = 0f;
            Armor = 0f;
            Damage = 1f;
            RangedDamage = 1f;
            MeleeDamage = 1f;
            AttackRange = 16f;
            AttackSpeed = 1f;
            Level = 1;
            Experience = 0;
        }
    }
}