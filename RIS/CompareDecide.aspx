<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CompareDecide.aspx.vb" Inherits="RIS.CompareDecide" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script src="<%# ResolveUrl("~/js/HideShow.js")%>" type="text/javascript"></script>
   <script type="text/javascript">
        function EvaluateProperty(propertyId) {
           // alert($("#chk" + propertyId).prop("checked"));
            var isChecked = 0;
            if ($("#chk" + propertyId).prop("checked")) {
                isChecked = 1;
            }
            UpdateProperty(propertyId, isChecked);
        }
         function UpdateProperty(propertyId, selected) {
             var dataValue = '{propertyID: "' + propertyId + '", selected: "' + selected + '", customerID: "' + $("#hidCustomerId").val() + '"}';
             
            $.ajax({
                url: "CompareDecide.aspx/UpdateProperty",
                type: "POST",
                dataType: "json",
                data: dataValue,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    var answer = msg.d;
                    //answer = answer.replaceAll("!", ",");
                    //answer = answer.substr(0, answer.length - 1);
                    //answer = answer.replace(/,(?=[^,]*$)/, ' and ');
                    //answer = answer.replaceAll(",", ", ");
                    //$("#" + lblId).text(answer);
                    //alert(answer);
                    return false;
                },
                error: function () { }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
  
    <asp:HiddenField ID="hidCustomerId" runat="server" />
    <asp:HiddenField ID="hidPropertyId" runat="server" />
    <asp:HiddenField ID="hidSelectedPropertyId" runat="server" />
    <asp:HiddenField ID="hidPropertyImage" runat="server" />
    <asp:HiddenField ID="hidValueMapProperty" runat="server" />
     <asp:HiddenField ID="hidValueMapPropertyContentImpacts1" runat="server" />
     <asp:HiddenField ID="hidValueMapPropertyContentImpacts2" runat="server" />
     <asp:HiddenField ID="hidValueMapPropertyContentImpacts3" runat="server" />

     <asp:HiddenField ID="hidValueMapPropertyContentBenefits1" runat="server" />
     <asp:HiddenField ID="hidValueMapPropertyContentBenefits2" runat="server" />
     <asp:HiddenField ID="hidValueMapPropertyContentBenefits3" runat="server" />
    <div class="right" id='maincontent'>
        <ul class='arrow-steps clearfix font-weight-bold'>
            <li class='step '>
                <span><a id="aOverView" runat="server" href='overview.aspx'>Overview</a></span>
            </li>
            <li class='step '>
                <a id="aCreateValueMap" runat="server" href='valuemap.html'><span><span class='first'>Step 1</span>
                    <span>View/Create<br />
                        Value Map</span></span></a>
            </li>
            <li class='step '>
                <a id="aCreateProperty" href='#' runat="server"><span><span class='first'>Step 2</span>
                    <span>Add/Evaluate<br />
                        Properties</span></span></a>
            </li>
            <li class='step active'>
                <a id="aCompareDecide" runat="server" href='#'><span><span class='first'>Step 3 </span>
                    <span>Discuss and<br />
                        Decide</span></span></a>
            </li>
        </ul>
        <div class='row'  style="margin-top:-35px;">
            <div class='col-md-7'>
                <h2><span id="lblCustomerName" runat="server"></span></h2>
            </div>
            <div class='col-md-5'>
                <div class='clearfix'>
                </div>
            </div>
        </div>
       <%-- <div class="row">
            <div class="col-md-12 font-weight-bold">
                <asp:CheckBox ID="chkIncludeArchieve" runat="server" AutoPostBack="true" Text="Include Archived" OnCheckedChanged="chkIncludeArchieve_CheckedChanged"/>
            </div>
        </div>--%>
        <div class="row" id="divViewProperty" runat="server">
        </div>
    </div>
        <style type="text/css">
            #home {
                background-color: #cecece;
            }

            .form-control:focus {
                border-color: inherit;
                -webkit-box-shadow: none;
                box-shadow: none;
            }
           
        </style>
    <style type="text/css">
        .validatorCalloutHighlight
        {
            background-color: pink !important;
            border-color: Red!important;
            border-width: 2px!important;
            border-style: Solid !important;
        }

        #ValidatorCalloutExtender1_popupTable, #ValidatorCalloutExtender2_popupTable, #ValidatorCalloutExtender3_popupTable, #ValidatorCalloutExtender4_popupTable, #ValidatorCalloutExtender5_popupTable
        {
            visibility: hidden!important;
        }
      #divAddProperty label {
              margin-bottom:0px!important;
          }
        .ranktitle {
            text-align:center;
           text-transform: uppercase;
        }
         .drophere {
    cursor: move;
      
    }

    .draghere {
    cursor: move;
    }
    .ui-draggable-dragging {
      background: gray;
    }
    </style>
    <script type="text/javascript">
    $(document).ready(function () {
      window.startPos = window.endPos = {};

      makeDraggable(); 

      $('.drophere').droppable({
        hoverClass: 'hoverClass',
        drop: function(event, ui) {
          var $from = $(ui.draggable),
              $fromParent = $from.parent(),
              $to = $(this).children(),
              $toParent = $(this);

          window.endPos = $to.offset();

          swap($from, $from.offset(), window.endPos, 0);
          swap($to, window.endPos, window.startPos, 1000, function() {
            $toParent.html($from.css({position: 'relative', left: '', top: '', 'z-index': ''}));
            $fromParent.html($to.css({position: 'relative', left: '', top: '', 'z-index': ''}));
              makeDraggable();
              var i = 1;
              var propertyRanks="";
              $(".ranktitle").each(function () {
                  $(this).text("Rank " + i);
                   propertyRanks +=$(this).attr("id")+","+i+";"
                  i = i + 1;
                 
              })
            //  alert(propertyRanks);
              UpdatePropertyRank(propertyRanks);
          });
        }
      });

      function makeDraggable() {
        $('.draghere').draggable({
          zIndex: 99999,
          revert: 'invalid',
          start: function(event, ui) {
            window.startPos = $(this).offset();
          }
        });
      }

      function swap($el, fromPos, toPos, duration, callback) {
        $el.css('position', 'absolute')
          .css(fromPos)
          .animate(toPos, duration, function() {
            if (callback) callback();
              });
          
        }
        $(function () {
            maxHeightPriorityButtons("streetaddress");
        })
        function maxHeightPriorityButtons(divClass) {
            var maxHeight = Math.max.apply(null, $("." + divClass).map(function () {
                return $(this).height();
            }).get());

           // alert(maxHeight);
            $("." + divClass).css("min-height", maxHeight);
           // $("#" + divClass).find(".btns .btn-default").css("min-height", maxHeight);
        }
        });
        function UpdatePropertyRank(propertyIds) {
             var dataValue = '{propertyIDs: "' + propertyIds + '", customerID: "' + $("#hidCustomerId").val() + '"}';
             
            $.ajax({
                url: "CompareDecide.aspx/UpdatePropertyRank",
                type: "POST",
                dataType: "json",
                data: dataValue,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    var answer = msg.d;
                    //answer = answer.replaceAll("!", ",");
                    //answer = answer.substr(0, answer.length - 1);
                    //answer = answer.replace(/,(?=[^,]*$)/, ' and ');
                    //answer = answer.replaceAll(",", ", ");
                    //$("#" + lblId).text(answer);
                    //alert(answer);
                    return false;
                },
                error: function () { }
            });
        }
        </script>

</asp:Content>
