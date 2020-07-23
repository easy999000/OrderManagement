using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Util
{
    public static class QueryConditionExtend
    {


        public static IQueryable<TSource> GetWhere<TSource, TCondition>(this IQueryable<TSource> sourceDB, PageQueryParam<TCondition> QueryParam)
        {

            #region 参数是否满足条件判断
            if (sourceDB == null)
            {
                return sourceDB;
            }
            if (QueryParam == null)
            {
                return sourceDB;
            }
            #endregion

            ///取参数类型
            Type QueryParamType = QueryParam.Condition.GetType();

            ///取db类型
            Type TSourceDBType = typeof(TSource);

            ///取参数字段
            var QueryTypeProperties = QueryParamType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            ///取db字段
            var TSourceDBProp = TSourceDBType.GetProperties(BindingFlags.Public | BindingFlags.Instance);


            foreach (var item in QueryTypeProperties)
            {
                sourceDB = GetWhere(sourceDB, QueryParam, TSourceDBProp, item);

            }

            return sourceDB;
        }

        private static IQueryable<TSource> GetWhere<TSource, TCondition>(
            IQueryable<TSource> sourceDB
            , PageQueryParam<TCondition> QueryParam
            , PropertyInfo[] TSourceDBProp
            , PropertyInfo QueryTypePropertyInfo
            )
        {
            ////查询条件特性
            QueryParamAttribute QueryParamAttr = null;


            #region 参数是否满足条件判断
            if (TSourceDBProp == null)
            {
                return sourceDB;
            }

            if (QueryTypePropertyInfo == null)
            {
                return sourceDB;
            }

            QueryParamAttr = QueryTypePropertyInfo.GetCustomAttribute<QueryParamAttribute>();

            if (QueryParamAttr == null)
            {
                return sourceDB;
            }

            if (QueryParamAttr.FieldName == null)
            {
                return sourceDB;
            }

            #endregion

            //// 作用的条件对象属性
            var TSourceProp = TSourceDBProp.First(o => o.Name == QueryParamAttr.FieldName);

            if (TSourceProp == null)
            {
                return sourceDB;
            }

            var QueryParamFieldValue = QueryTypePropertyInfo.GetValue(QueryParam.Condition);


            if (QueryTypePropertyInfo.PropertyType.IsDefaultValue(QueryParamFieldValue))
            {
                return sourceDB;
            }


            Expression mainExpression = null;

            ParameterExpression FunParam = Expression.Parameter(typeof(TSource));

            switch (QueryParamAttr.OperatorType)
            {
                case OperatorType.Equal:
                case OperatorType.NotEqual:
                case OperatorType.GreaterThan:
                case OperatorType.GreaterThanOrEqual:
                case OperatorType.LessThan:
                case OperatorType.LessThanOrEqual:
                    mainExpression = GetBinaryExpression(sourceDB
                                , QueryParam, TSourceDBProp
                                , QueryTypePropertyInfo, QueryParamAttr
                                , TSourceProp, QueryParamFieldValue
                                , FunParam
                                );
                    break;

                case OperatorType.Like:
                    mainExpression = GetMethodCallExpression_Like(sourceDB
                                , QueryParam, TSourceDBProp
                                , QueryTypePropertyInfo, QueryParamAttr
                                , TSourceProp, QueryParamFieldValue
                                , FunParam
                                );

                    break;
                case OperatorType.StartWith:
                    mainExpression = GetMethodCallExpression_LikeStart(sourceDB
                                , QueryParam, TSourceDBProp
                                , QueryTypePropertyInfo, QueryParamAttr
                                , TSourceProp, QueryParamFieldValue.ToString() + "%"
                                , FunParam
                                );
                    break;
                case OperatorType.EndWith:
                    mainExpression = GetMethodCallExpression_LikeStart(sourceDB
                                , QueryParam, TSourceDBProp
                                , QueryTypePropertyInfo, QueryParamAttr
                                , TSourceProp, "%" + QueryParamFieldValue.ToString()
                                , FunParam
                                );
                    break;
                case OperatorType.In:
                    break;

                default:
                    break;
            }
            if (mainExpression == null)
            {
                return sourceDB;
            }

            //System.Linq.Expressions.ConstantExpression ModelValue = Expression.Constant(QueryMemberInfo.GetMemberValue(QueryParam)) ;

            Expression<Func<TSource, bool>> whereExpression = Expression.Lambda<Func<TSource, bool>>(mainExpression, FunParam);


            sourceDB = sourceDB.Where(whereExpression);


            return sourceDB;
        }

        public static BinaryExpression GetBinaryExpression<TSource, TCondition>(
            IQueryable<TSource> sourceDB
            , PageQueryParam<TCondition> QueryParam
            , PropertyInfo[] TSourceDBProp
            , MemberInfo QueryTypeMemberInfo
            , QueryParamAttribute QueryParamAttr
            , PropertyInfo TSourceProp
            , object QueryParamFieldValue
            , ParameterExpression FunParam
            )
        {
            BinaryExpression mainExpression = null;


             
            MemberExpression LeftProp = Expression.Property(FunParam, TSourceProp.Name);
            //System.Linq.Expressions.ParameterExpression RightProp = Expression.Parameter(QueryMemberInfo.ReflectedType);
            ConstantExpression RightProp = Expression.Constant(QueryParamFieldValue);



            switch (QueryParamAttr.OperatorType)
            {
                case OperatorType.Equal:
                    mainExpression = Expression.MakeBinary(ExpressionType.Equal, LeftProp, RightProp);
                    break;
                case OperatorType.NotEqual:
                    mainExpression = Expression.MakeBinary(ExpressionType.NotEqual, LeftProp, RightProp);
                    break;

                case OperatorType.GreaterThan:
                    mainExpression = Expression.MakeBinary(ExpressionType.GreaterThan, LeftProp, RightProp);
                    break;
                case OperatorType.GreaterThanOrEqual:
                    mainExpression = Expression.MakeBinary(ExpressionType.GreaterThanOrEqual, LeftProp, RightProp);
                    break;
                case OperatorType.LessThan:
                    mainExpression = Expression.MakeBinary(ExpressionType.LessThan, LeftProp, RightProp);
                    break;
                case OperatorType.LessThanOrEqual:
                    mainExpression = Expression.MakeBinary(ExpressionType.LessThanOrEqual, LeftProp, RightProp);
                    break;
                default:
                    break;
            }



            return mainExpression;
        }

        public static MethodCallExpression GetMethodCallExpression_Like<TSource, TCondition>(
            IQueryable<TSource> sourceDB
            , PageQueryParam<TCondition> QueryParam
            , PropertyInfo[] TSourceDBProp
            , MemberInfo QueryTypeMemberInfo
            , QueryParamAttribute QueryParamAttr
            , PropertyInfo TSourceProp
            , object QueryParamFieldValue
            , ParameterExpression FunParam
            )
        {
            MethodCallExpression mainExpression = null;




            MemberExpression LeftProp = Expression.Property(FunParam, TSourceProp.Name);
            //System.Linq.Expressions.ParameterExpression RightProp = Expression.Parameter(QueryMemberInfo.ReflectedType);
            ConstantExpression RightProp = Expression.Constant(QueryParamFieldValue);


            mainExpression = Expression.Call(LeftProp,
               typeof(string).GetMethod("Contains", new Type[] { typeof(string) }),
              RightProp);

            return mainExpression;


        }
        public static MethodCallExpression GetMethodCallExpression_LikeStart<TSource, TCondition>(
            IQueryable<TSource> sourceDB
            , PageQueryParam<TCondition> QueryParam
            , PropertyInfo[] TSourceDBProp
            , MemberInfo QueryTypeMemberInfo
            , QueryParamAttribute QueryParamAttr
            , PropertyInfo TSourceProp
            , object QueryParamFieldValue
            , ParameterExpression FunParam
            )
        {
            MethodCallExpression mainExpression = null;


            MemberExpression LeftProp = Expression.Property(FunParam, TSourceProp.Name);
            var itemParameter = Expression.Parameter(typeof(TSource), "item");
            var functions = Expression.Property(null, typeof(EF).GetProperty(nameof(EF.Functions)));
            var like = typeof(DbFunctionsExtensions).GetMethod(nameof(DbFunctionsExtensions.Like), new Type[] { functions.Type, typeof(string), typeof(string) });
            //Expression expressionProperty = Expression.Property(itemParameter, TSourceProp.Name);

            //if (TSourceProp.PropertyType != typeof(string))
            //{
            //    expressionProperty = Expression.Call(expressionProperty, typeof(object).GetMethod(nameof(object.ToString), new Type[0]));
            //}


            mainExpression = Expression.Call(
                       null,
                       like,
                       functions,
                       LeftProp,
                       Expression.Constant(QueryParamFieldValue));

            return mainExpression;


        }


    }
}
